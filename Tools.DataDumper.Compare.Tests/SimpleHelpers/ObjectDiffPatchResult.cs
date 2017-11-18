using System;
using Newtonsoft.Json.Linq;

namespace Tools.DataDumper.Compare.Tests.SimpleHelpers
{
    /// <summary>
    ///     Result of a diff operation between two objects
    /// </summary>
    public class ObjectDiffPatchResult
    {
        /// <summary>
        ///     If the compared objects are equal.
        /// </summary>
        /// <value>true if the obects are equal; otherwise, false.</value>
        public bool AreEqual => OldValues == null && NewValues == null;

        /// <summary>
        ///     The type of the compared objects.
        /// </summary>
        /// <value>The type of the compared objects.</value>
        public Type Type { get; set; }

        /// <summary>
        ///     The values modified in the original object.
        /// </summary>
        public JObject OldValues { get; set; }

        /// <summary>
        ///     The values modified in the updated object.
        /// </summary>
        public JObject NewValues { get; set; }
    }
}