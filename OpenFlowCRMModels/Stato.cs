namespace OpenFlowCRMModels
{

    public enum STATO_PARTITA
    {
        PENDING = 0,
        ORDINE_CONFERMATO = 10,
        PRODOTTA = 20,
        CARICATA = 30,
        CONSEGNATA = 40
    }

    public enum STATO_LOTTO
    {
        PIANIFICATO = 0,
        IN_PRODUZIONE = 10,
        PRODOTTO = 20
    }

    public static class PathProvider
    {
        public static DirectoryInfo TryGetSolutionDirectoryInfo(string currentPath = null)
        {
            var directory = new DirectoryInfo(
                currentPath ?? Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
            return directory;
        }
    }

}
