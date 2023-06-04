# unity-simple-line-chart
Easy to use, configurable line chart for Unity game engine.

Tested with Unity 2022.2.17f1

Licensed under MIT license

![alt text](https://github.com/modernlearner/unity-simple-line-chart/blob/main/Documentation~/screenshot.jpg)


## Usage

1. Add the `Chart` prefab to your scene
2. Update the minimum and maximum values of X and Y axis of the chart. These are used for clamping the values of the data points.
3. Clear the `ChartData` list of the `Chart` component prefab
3. Add the `Chart` component to your script
4. Add data points to the chart, one by one or in bulk
5. Manually call the render method of the chart if you want to render the chart after adding data points in bulk

Setting the minimum and maximum values of the X and Y axis of the chart is important. If you don't set these values, the chart will not render properly.

If you do not want to set them, you can set the `recalculateMinMaxValues` component value to `true` and the chart will automatically calculate the minimum and maximum values of the X and Y axis of the chart right before rendering.

```csharp
public class ExampleScreen : MonoBehaviour
{
    public Chart exampleChart;

    void Start()
    {
        // Adding one data point and rendering the chart
        exampleChart.AddData(new Vector2(2021, 1));

        // Adding one data point without rendering the chart
        exampleChart.AddData(new Vector2(2022, 1), false);

        // Adding multiple data points without rendering the chart
        exampleChart.chartData.AddRange(
            new List<Vector2>
            {
                new(2023, 3),
                new(2024, 2),
                new(2025, 3),
                new(2026, 2)
            }
        );
        
        // Automatically recalculate the minimum and maximum values of the X and Y axis of the chart before rendering
        exampleChart.recalculateMinMaxValues = true;

        // Render the chart
        exampleChart.RenderChart();
    }
}
```
