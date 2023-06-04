using System.Collections.Generic;
using UnityEngine;

namespace DataVisualization.Charts
{
    [SelectionBase]
    public class Chart : MonoBehaviour
    {
        [Header("Horizontal")]
        [Tooltip("The minimum value for X axis")]
        public float minXValue;
        [Tooltip("The maximum value for X axis")]
        public float maxXValue;
        public Transform startX;
        public Transform endX;
        public List<Transform> pointsOnX;

        [Header("Vertical")]
        [Tooltip("The minimum value for Y axis")]
        public float minYValue;
        [Tooltip("The maximum value for Y axis")]
        public float maxYValue;
        public Transform startY;
        public Transform endY;
        public List<Transform> pointsOnY;

        [Header("Data")]
        public List<Vector2> chartData = new();
        public LineRenderer lineRenderer;
        [Tooltip("Recalculates the minimum and maximum values for X and Y axes before rendering")]
        public bool recalculateMinMaxValues = false;

        public void AddData(Vector2 dataPoint, bool renderChart = true)
        {
            chartData.Add(dataPoint);

            if (renderChart) RenderChart();
        }

        [ContextMenu("Chart/Render Chart")]
        public void RenderChart()
        {
            if (recalculateMinMaxValues)
            {
                RecalculateMinMaxValues();
            }
            Debug.Log("Rendering Chart");
            lineRenderer.positionCount = chartData.Count;

            for (int i = 0; i < chartData.Count; i++)
            {
                // Prevent user from inserting numbers off-limits
                chartData[i] = new Vector2
                (
                    Mathf.Clamp(chartData[i].x, minXValue, maxXValue),
                    Mathf.Clamp(chartData[i].y, minYValue, maxYValue)
                );

                var x = ValueToChartPosition(startX.localPosition, endX.localPosition, maxXValue, chartData[i].x);
                var y = ValueToChartPosition(startY.localPosition, endY.localPosition, maxYValue, chartData[i].y);

                // Inserting values
                lineRenderer.SetPosition(i, new Vector2(x, y));
            }
        }

        /// <summary>
        /// Manipulates point on X & Y lines of the chart
        /// </summary>
        [ContextMenu("Chart/Manipulate")]
        private void DoManipulation()
        {
            ManipulatePoints(startX.position, endX.position);
            ManipulatePoints(startX.position, endX.position, false);
        }

        /// <summary>
        /// Manipulates point in given direction
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="horizontally"></param>
        private void ManipulatePoints(Vector3 startPoint, Vector3 endPoint, bool horizontally = true)
        {
            Debug.LogFormat("Manipulating points from {0} to {1}", startPoint, endPoint);
            var distance = Vector3.Distance(startPoint, endPoint);
            var spaceBetweenEachPoint = distance / pointsOnX.Count;
            if (horizontally)
            {
                var pose = new Vector3(startPoint.x + spaceBetweenEachPoint, startPoint.y, startPoint.z);
                for (int i = 0; i < pointsOnX.Count; i += 1)
                {
                    Debug.LogFormat("Setting point, horizontally {0}, {1}, {2}", i, pointsOnX[i].position, pose);
                    pointsOnX[i].position = pose;
                    pose.x += spaceBetweenEachPoint;
                }
            }
            else
            {
                var pose = new Vector3(startPoint.x, startPoint.y + spaceBetweenEachPoint, startPoint.z);
                for (int i = 0; i < pointsOnY.Count; i += 1)
                {
                    Debug.LogFormat("Setting point, not horizontally {0}, {1}, {2}", i, pointsOnY[i].position, pose);
                    pointsOnY[i].position = pose;
                    pose.y += spaceBetweenEachPoint;
                }
            }
        }


        /// <summary>
        /// Defines how much of the given line should be occupied by the data X or Y
        /// </summary>
        /// <param name="startpoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="max"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private float ValueToChartPosition(Vector3 startpoint, Vector3 endPoint, float max, float value)
        {
            var percentageInChart = value * 100 / maxXValue;
            var distanceBetweenTwoPointsOnChart = Vector3.Distance(startpoint, endPoint);
            return distanceBetweenTwoPointsOnChart * percentageInChart / 100;
        }

        private void RecalculateMinMaxValues()
        {
            var minX = chartData[0].x;
            var maxX = chartData[0].x;
            var minY = chartData[0].y;
            var maxY = chartData[0].y;

            for (int i = 1; i < chartData.Count; i++)
            {
                Debug.LogFormat("MinX: {0}, MaxX: {1}, MinY: {2}, MaxY: {3}", minX, maxX, minY, maxY);
                minX = Mathf.Min(minX, chartData[i].x);
                maxX = Mathf.Max(maxX, chartData[i].x);
                minY = Mathf.Min(minY, chartData[i].y);
                maxY = Mathf.Max(maxY, chartData[i].y);
            }

            minXValue = minX;
            maxXValue = maxX;
            minYValue = minY;
            maxYValue = maxY;
        }

        private void OnDrawGizmos()
        {
            // X Axis
            Gizmos.color = Color.red;

            Gizmos.matrix = startX.localToWorldMatrix;
            Gizmos.DrawCube(Vector3.zero, Vector3.one * 10);

            Gizmos.matrix = endX.localToWorldMatrix;
            Gizmos.DrawCube(Vector3.zero, Vector3.one * 10);

            // Y Axis
            Gizmos.color = Color.green;

            Gizmos.matrix = startY.localToWorldMatrix;
            Gizmos.DrawCube(Vector3.zero, Vector3.one * 10);

            Gizmos.matrix = endY.localToWorldMatrix;
            Gizmos.DrawCube(Vector3.zero, Vector3.one * 10);
        }
    }
}