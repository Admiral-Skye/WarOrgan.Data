using WarOrgan.Data.Common.Utils;
using WarOrgan.Data.Common.Validation;
using WarOrgan.Model.DataSet;

var defaultDatasetId = "Warhammer40k11e";
var defaultVersion = new Version(1, 0, 0, 5);
var defaultBuildDirectory = AppContext.BaseDirectory;

var datasetInfo = new DataSetInfo(
    Id: defaultDatasetId,
    Version: defaultVersion,
    DataSetFormatVersion: DataSetInfo.CurrentDataSetFormatVersion,
    PublishDate: DateTime.UtcNow);

// Setup paths
var assetsFolder = Path.Combine(AppContext.BaseDirectory, "Assets", defaultDatasetId).EnsureDirectoryExists();
var outputFolder = Path.Combine(AppContext.BaseDirectory, "Output").EnsureDirectoryExists();
var rostersFolder = Path.Combine(assetsFolder, "Rosters").EnsureDirectoryExists();

try
{
    datasetInfo
        .CreateDatasetBuildModel(assetsFolder)
        .AddRosters(rostersFolder)
        .IncludeReadme(assetsFolder)
        .Validate()
        .PublishToZipArchive(outputFolder);
}
catch (Exception ex)
{
    LoggingBuffer.Error($"Dataset build failed: {ex.Message}");
}

LoggingBuffer.Print(waitForInput: false);
return LoggingBuffer.ErrorCount == 0 ? 0 : 1;