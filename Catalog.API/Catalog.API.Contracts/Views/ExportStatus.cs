namespace Catalog.API.Contracts.Views
{
    /// <summary>
    /// Describes the status of the current catalog export
    /// </summary>
    public enum ExportStatus
    {
        Undefined = 0,
        InQueue = 1,
        InProgress = 2,
        Completed = 3
    }
}