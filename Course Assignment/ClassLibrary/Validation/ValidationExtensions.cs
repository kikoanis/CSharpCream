namespace ClassLibrary.Validation

{
    public static class ValidationExtensions
    {
        public static void Validate<TEntity>(this IValidated entity)
        {
            var results = Microsoft.Practices.EnterpriseLibrary.Validation.Validation.Validate<TEntity>((TEntity)entity);
            if (!results.IsValid)
                throw new ValidationException(results);
        }


    }
}
