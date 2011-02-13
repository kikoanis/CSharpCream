namespace DataAccessLayer
{
    internal class ProviderInterceptor : NHibernate.EmptyInterceptor
    {

        #region Public Methods

        public override bool OnFlushDirty(object entity,
                                            object id,
                                            object[] currentState,
                                            object[] previousState,
                                            string[] propertyNames,
                                            NHibernate.Type.IType[] types)
        {

            if (!(previousState == null))
            {
                for (int i = 0; i < propertyNames.Length; i++)
                {
                    if (previousState[i] != null)
                    {
                        System.Diagnostics.Debug.WriteLine(propertyNames[i] + ": Previous Value = " +
                                                           previousState[i] + ", Current Value = " +
                                                           (currentState[i]==null?"null":currentState[i].ToString()));
                    }

                }
            }

            return base.OnFlushDirty(entity, id, currentState, previousState, propertyNames, types);
        }

        #endregion

    }
}
