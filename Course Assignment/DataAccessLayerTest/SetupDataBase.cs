using MbUnit.Framework;

namespace DataAccessLayerTest
{
	///<summary>
	///
	///</summary>
	[TestFixture]
    public class SetupDataBase
    {
		///<summary>
		///
		///</summary>
		[Test]
        [Ignore]
        public void CreateDatabase()
        {
            var cfg = new NHibernate.Cfg.Configuration();

            cfg.Configure();

            var schema = new NHibernate.Tool.hbm2ddl.SchemaExport(cfg);

            schema.Execute(true, false, false);

        }
    }
}
