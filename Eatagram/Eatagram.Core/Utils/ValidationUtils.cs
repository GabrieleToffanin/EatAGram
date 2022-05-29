using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Utils
{
    public static class ValidationUtils
    {
        /// <summary>
        /// Helper method for validate entity via Attributes
        /// </summary>
        /// <typeparam name="TEntity">Current type of the entity to validate</typeparam>
        /// <param name="entityToValidate">The actual entity to validate</param>
        /// <returns>A list of ValidationResult if empty validation Ok else some value are invalid</returns>
        /// <exception cref="ArgumentNullException">Throws if the entity is null</exception>
        public static IEnumerable<ValidationResult> Validate<TEntity>(TEntity entityToValidate)
        {
            //Checks if entity is null
            if (entityToValidate == null) throw new ArgumentNullException(nameof(entityToValidate));

            //Creates a validation context based on current entity
            ValidationContext context = new ValidationContext(entityToValidate);

            //Creates a list who'll be fulled with error if any
            IList<ValidationResult> validationResults = new List<ValidationResult>();

            //Tries to validate the current object
            Validator.TryValidateObject(entityToValidate, context, validationResults, true);

            //Returns the current errors list
            return validationResults;
        }

    }
}
