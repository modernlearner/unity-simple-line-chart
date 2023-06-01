[![license](https://img.shields.io/badge/license-MIT-brightgreen.svg?style=flat-square)](https://github.com/dimmpixeye/InspectorFoldoutGroup/blob/master/LICENSE)

# unity-simple-line-chart
Easy to use, configurable line chart for Unity game engine.

Tested with Unity 2022.2.17f1

Licensed under MIT license. See LICENSE file in the project root folder.

![alt text](https://github.com/modernlearner/unity-simple-line-chart/blob/main/ss_01.jpg)


Adding new record:

```csharp
public class ExampleScreen : MonoBehaviour
{
    public Chart exampleChart;

    void Start()
    {
        // Adding one data point and rendering the chart
        exampleChart.AddData(new TwoDimensionalData(2021, 1), true);
        
        // Adding one data point without rendering the chart
        exampleChart.AddData(new TwoDimensionalData(2022, 1), false);

        // Adding multiple data points without rendering the chart
        exampleChart.chartData.AddRange({ new TwoDimensionalData(2023, 3), new TwoDimensionalData(2024, 2) });
        exampleChart.chartData.AddRange(
            new List<TwoDimensionalData>
            {
                new TwoDimensionalData(2025, 3),
                new TwoDimensionalData(2026, 2)
            }
        );
        
        // Render the chart
        exampleChart.RenderChart();
    }
}
```
