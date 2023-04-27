using FractalPlatform.Database.Converter;

namespace FractalPlatform.Converter
{
    public class CollectionsToTablesConverter
    {
        #region Members

        private MSSQLConverter _converter;

        private string _appName;

        private string _schema;

        const string _deploymentPath = @"..\..\..\..";

        const string _assemblyName = "FractalPlatform.Converter.Examples";

        private bool _isAddConstraints;

        #endregion

        #region Constructors

        public CollectionsToTablesConverter(BaseRepository repository,
                                            string appName,
                                            string schema,
                                            bool isAddConstraints)
        {
            _converter = new MSSQLConverter(repository, schema);

            _appName = appName;

            _schema = schema;

            _isAddConstraints = isAddConstraints;
        }

        #endregion

        public void Run()
        {
            _converter.CreateDatabase(true);

            _converter.CreateSchemaIfNotExists(_schema);

            var dbPath = $@"{_deploymentPath}\{_assemblyName}\Databases\{_appName}\";

            _converter.ProcessDocuments(dbPath);
            
            _converter.RenameTables();

            if(_isAddConstraints)
            {
                _converter.AddConstraints();
            }
        }
    }
}
