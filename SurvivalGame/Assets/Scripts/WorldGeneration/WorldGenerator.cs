using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class WorldGenerator : MonoBehaviour
{
    public int seed = 0;

    [Header("Generation Variables")]
    [SerializeField] private bool useManhattanDistance = false;
    [SerializeField] private int mapSize = 100;
    [SerializeField] private int edgeRadius = 5;
    [SerializeField] private List<Color> regionColors;

    [EasyAttributes.Preview] private Texture2D regions;
    private List<Vector2Int> centroids;
    private List<ColoredVector2Int> pix;

    [EasyAttributes.Button]
    private void GenerateNewMap()
    {
        InitializeRegions();
        EqualizeRegions();
        EqualizeRegions();
        PerlinNoiseBorder();
        RemoveAllBlackSpots();
        RemoveAllBlackSpots();
    }

    [EasyAttributes.Button]
    private void RandomSeed()
    {
        int newSeed = 0;

        for (int i = 0; i < 6; i++)
        {
            newSeed *= 10;
            newSeed += Random.Range(0, 9);
        }

        seed = newSeed;
    }

    #region HigherFunctions
    /// <summary>
    /// Generate random points based on the seed, then use Voronoi tesselation to generate the regions
    /// </summary>
    private void InitializeRegions()
    {
        GenerateRandomPoints();
        GenerateRegions();
    }

    /// <summary>
    /// Generate new centroids, then use Voronoi tesselation to generate the regions again
    /// </summary>
    private void EqualizeRegions()
    {
        GenerateCentroids();
        GenerateRegions();
    }

    /// <summary>
    /// Returns the 2 outer borders of all regions
    /// </summary>
    /// <returns></returns>
    private List<ColoredBorders> ColorRegionBorders()
    {
        //Setup
        List<List<ColoredVector2Int>> regionList = GetRegions();
        List<ColoredBorders> cBorders = new List<ColoredBorders>();

        for (int i = 0; i < regionList.Count; i++)
        {
            List<ColoredVector2Int> l = regionList[i];
            ColoredBorders cBorder = new ColoredBorders(regionList[i][0].color);

            for (int r = 0; r < l.Count; r++)
            {
                ColoredVector2Int currentCVec = l[r];
                List<Vector2Int> n = currentCVec.vector2Int.GetDirectNeighbours(mapSize);

                //Check if it's next to mapborder
                if (n.Count < 4)
                {
                    cBorder.outerBorder.Add(currentCVec.vector2Int);
                    continue;
                }

                //Checks if it's next to another color
                for (int t = 0; t < n.Count; t++)
                {
                    if(pix.Exists(x => (x.vector2Int == n[t]) && (x.color != currentCVec.color)))
                    {
                        cBorder.outerBorder.Add(currentCVec.vector2Int);
                        break;
                    }
                }
            }

            for (int r = 0; r < l.Count; r++)
            {
                ColoredVector2Int currentCVec = l[r];
                List<Vector2Int> n = currentCVec.vector2Int.GetDirectNeighbours(mapSize);

                //Checks if it's next to another color
                for (int t = 0; t < n.Count; t++)
                {
                    if (pix.Exists(x => (x.vector2Int == n[t]) && (x.color != currentCVec.color)))
                    {
                        cBorder.innerBorder.Add(currentCVec.vector2Int);
                        break;
                    }
                }
            }

            cBorders.Add(cBorder);
        }

        return cBorders;
    }

    /// <summary>
    /// Makes the border more jaggy
    /// </summary>
    private void PerlinNoiseBorder()
    {
        var coloredBorders = ColorRegionBorders();
        
        //Disable some part of the border
        for (int i = 0; i < coloredBorders.Count; i++)
        {
            ColoredBorders cBorder = coloredBorders[i];

            for (int r = 0; r < cBorder.innerBorder.Count; r++)
            {
                float value = Random.value;
                SetPixColor((value > 0.5f)? cBorder.innerBorder[r] : cBorder.outerBorder[r], cBorder.color);
                
            }

            for (int r = 0; r < cBorder.outerBorder.Count; r++)
            {
                SetPixColor(cBorder.outerBorder[r], (Random.value > 0.5f) ? cBorder.color : Color.black);
            }
        }

        ApplyPixColorsToTexture();
    }

    /// <summary>
    /// Colors all black pixels to one of their direct neighbours
    /// </summary>
    private void RemoveAllBlackSpots()
    {
        for (int i = 0; i < pix.Count; i++)
        {
            if (pix[i].color == Color.black)
            {
                List<Vector2Int> nBours = pix[i].vector2Int.GetDirectNeighbours(mapSize);

                Color c = Color.black;
                for (int r = 0; r < nBours.Count; r++)
                {
                    Color px = regions.GetPixel(nBours[r].x, nBours[r].y);
                    if (px != Color.black)
                    {
                        c = px; 
                    }
                }
                SetPixColor(pix[i].vector2Int, c);
            }
        }

        ApplyPixColorsToTexture();
    }
    #endregion

    #region Generation
    /// <summary>
    /// Generate a few random points
    /// </summary>
    private void GenerateRandomPoints()
    {
        Random.InitState(seed);
        centroids = new List<Vector2Int>();

        for (int i = 0; i < regionColors.Count; i++)
        {
            centroids.Add(new Vector2Int(Random.Range(0 + edgeRadius, mapSize - edgeRadius), Random.Range(0 + edgeRadius, mapSize - edgeRadius)));
        }
    }

    /// <summary>
    /// Generate the regions using Voronoi tesselation
    /// </summary>
    private void GenerateRegions()
    {
        //Setup
        pix = new List<ColoredVector2Int>(mapSize * mapSize);

        //Create a grid and set pixel color
        for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                pix.Add(new ColoredVector2Int(pos, regionColors[(useManhattanDistance)? GetIndexOfClosestManhattanPoint(pos, centroids) : GetIndexOfClosestEuclideanPoint(pos, centroids)]));
            }
        }

        ApplyPixColorsToTexture();
    }

    /// <summary>
    /// Generate new centroids
    /// </summary>
    private void GenerateCentroids()
    {
        //Setup
        List<List<ColoredVector2Int>> regionList = GetRegions();

        //Clear old centroids
        centroids.Clear();

        //Set the calculated centroid color to black (for visualisation)
        for (int i = 0; i < regionList.Count; i++)
        {
            Vector2Int centroid = GetCentroid(regionList[i]);
            centroids.Add(centroid);
            SetPixColor(centroid, Color.black);
        }

        ApplyPixColorsToTexture();
    }
    #endregion

    /// <summary>
    /// Gets all regions and puts them in a list of lists
    /// </summary>
    /// <returns>Returns all regions</returns>
    private List<List<ColoredVector2Int>> GetRegions()
    {
        List<List<ColoredVector2Int>> regionList = new List<List<ColoredVector2Int>>();

        for (int i = 0; i < regionColors.Count; i++)
        {
            regionList.Add(new List<ColoredVector2Int>());
        }

        //Sets all different color in a list
        for (int i = 0; i < regionColors.Count; i++)
        {
            regionList[i].AddRange(pix.FindAll(x => x.color == regionColors[i]));
        }

        return regionList;
    }
    
    /// <summary>
    /// Adds a random color to the regions color list (basically adding an extra region)
    /// </summary>
    [EasyAttributes.Button()]
    private void AddRegion()
    {
        Color c;
        do
        {
            c = new Color(Random.value, Random.value, Random.value);
        } while (regionColors.Contains(c));

        regionColors.Add(c);
    }

    [EasyAttributes.Button]
    private void SaveToPng()
    {
        regions.SaveToPNG();
    }

    /// <summary>
    /// Set the pixel color at the desired x,y
    /// </summary>
    /// <param name="vec">Coordinate</param>
    /// <param name="col">Color</param>
    private void SetPixColor(Vector2Int vec, Color col)
    {
        int index = pix.FindIndex(x => x.vector2Int == vec);
        pix[index] = new ColoredVector2Int(vec, col);
    }

    /// <summary>
    /// Apply colors to regions image to visualize the generator
    /// </summary>
    private void ApplyPixColorsToTexture()
    {
        Color[] pixels = new Color[mapSize * mapSize];
        regions = new Texture2D(mapSize, mapSize);


        for (int i = 0; i < pix.Count; i++)
        {
            ColoredVector2Int t = pix[i];
            pixels[t.vector2Int.y * mapSize + t.vector2Int.x] = t.color;
        }

        regions.SetPixels(pixels);
        regions.Apply();
    }

    #region Math

    private int GetIndexOfClosestManhattanPoint(Vector2Int coord, List<Vector2Int> points)
    {
        int index = 0;
        int closestDistance = int.MaxValue;

        for (int i = 0; i < points.Count; i++)
        {
            int dist = (Mathf.Abs(points[i].x - coord.x) + Mathf.Abs(points[i].y - coord.y));
            if (dist < closestDistance)
            {
                closestDistance = dist;
                index = i;
            }
        }

        return index;
    }

    private int GetIndexOfClosestEuclideanPoint(Vector2Int coord, List<Vector2Int> points)
    {
        int index = 0;
        float closestDistance = float.MaxValue;

        for (int i = 0; i < points.Count; i++)
        {
            float dist = Mathf.Sqrt(Mathf.Pow(points[i].x - coord.x, 2) + Mathf.Pow(points[i].y - coord.y, 2));

            if (dist < closestDistance)
            {
                closestDistance = dist;
                index = i;
            }
        }

        return index;
    }

    private Vector2Int GetCentroid(List<ColoredVector2Int> pixelArea)
    {
        int xTotal = 0;
        int yTotal = 0;
        int areaSize = pixelArea.Count;


        for (int i = 0; i < pixelArea.Count; i++)
        {
            xTotal += pixelArea[i].vector2Int.x;
            yTotal += pixelArea[i].vector2Int.y;
        }

        return new Vector2Int(xTotal / areaSize, yTotal / areaSize);


        //float accumulatedArea = 0.0f;
        //float centerX = 0.0f;
        //float centerY = 0.0f;

        //for (int i = 0, j = poly.Count - 1; i < poly.Count; j = i++)
        //{
        //    float temp = poly[i].vector2Int.x * poly[j].vector2Int.y - poly[j].vector2Int.x * poly[i].vector2Int.y;
        //    accumulatedArea += temp;
        //    centerX += (poly[i].vector2Int.x + poly[j].vector2Int.x) * temp;
        //    centerY += (poly[i].vector2Int.y + poly[j].vector2Int.y) * temp;
        //}

        //if (Mathf.Abs(accumulatedArea) < 1E-7f)
        //    return Vector2Int.zero;  // Avoid division by zero

        //accumulatedArea *= 3f;
        //return new Vector2Int(Mathf.Abs((int)(centerX / accumulatedArea)), Mathf.Abs((int)(centerY / accumulatedArea)));
    }

    #endregion

    private struct ColoredVector2Int
    {
        public Vector2Int vector2Int;
        public Color color;

        public ColoredVector2Int(Vector2Int vec, Color c)
        {
            vector2Int = vec;
            color = c;
        }
    }

    private struct ColoredBorders
    {
        public List<Vector2Int> innerBorder;
        public List<Vector2Int> outerBorder;
        public Color color;

        public ColoredBorders(Color c)
        {
            innerBorder = new List<Vector2Int>();
            outerBorder = new List<Vector2Int>();
            color = c;
        }

        public ColoredBorders(List<Vector2Int> inner, List<Vector2Int> outer, Color c)
        {
            innerBorder = inner;
            outerBorder = outer;
            color = c;
        }
    }
}
