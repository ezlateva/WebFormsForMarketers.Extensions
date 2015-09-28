using System;
using System.Collections.Generic;

namespace WebFormsForMarketers.Extensions.Processors
{
    public class FormData
    {
        /// <summary>
        /// Gets or sets the form identifier.
        /// </summary>
        /// <value>
        /// The form identifier.
        /// </value>
        public string FormId { get; set; }

        /// <summary>
        /// Gets or sets the fields.
        /// </summary>
        /// <value>
        /// The fields.
        /// </value>
        public IEnumerable<FormField> Fields { get; set; } 
    }
}