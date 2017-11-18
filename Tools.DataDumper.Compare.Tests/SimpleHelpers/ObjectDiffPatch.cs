#region *   License     *

/*
    SimpleHelpers - ObjectDiffPatch   

    Copyright © 2014 Khalid Salomão

    Permission is hereby granted, free of charge, to any person
    obtaining a copy of this software and associated documentation
    files (the “Software”), to deal in the Software without
    restriction, including without limitation the rights to use,
    copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the
    Software is furnished to do so, subject to the following
    conditions:

    The above copyright notice and this permission notice shall be
    included in all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND,
    EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
    OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
    NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
    HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
    WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
    FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
    OTHER DEALINGS IN THE SOFTWARE. 

    License: http://www.opensource.org/licenses/mit-license.php
    Website: https://github.com/khalidsalomao/SimpleHelpers.Net
 */

#endregion

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tools.DataDumper.Compare.Tests.SimpleHelpers
{
    public class ObjectDiffPatch
    {
        // ReSharper disable once InconsistentNaming
        private const string PREFIX_ARRAY_SIZE = "@@ Count";
        // ReSharper disable once InconsistentNaming
        private const string PREFIX_REMOVED_FIELDS = "@@ Removed";

        private static JsonSerializerSettings _defaultSerializerSettings;

        private static Func<JsonSerializer> _defaultSerializer;

        /// <summary>
        ///     Gets or sets the default newtonsoft json serializer factory function.
        /// </summary>
        public static Func<JsonSerializer> DefaultSerializer
        {
            get { return _defaultSerializer ?? InternalDefaultSerializer; }
            set { _defaultSerializer = value; }
        }

        /// <summary>
        ///     Gets or sets the default newtonsoft json serializer settings.
        /// </summary>
        public static JsonSerializerSettings DefaultSerializerSettings
        {
            get { return _defaultSerializerSettings ?? (_defaultSerializerSettings = InternalDefaultSerializerSettings()); }
            set { _defaultSerializerSettings = value; }
        }

        /// <summary>
        ///     Compares two objects and generates the differences between them.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <param name="updated">The updated.</param>
        /// <returns>The result of the diff operation</returns>
        public static ObjectDiffPatchResult GenerateDiff<T1, T2>(T1 original, T2 updated) where T1 : class where T2 : class
        {
            // ensure the serializer will not ignore null values
            var writer = DefaultSerializer();
            // parse our objects
            var originalJson = original != null ? JObject.FromObject(original, writer) : null;
            var updatedJson = updated != null ? JObject.FromObject(updated, writer) : null;

            // analyse their differences!
            return GenerateDiff(originalJson, updatedJson, typeof (T1));
        }

        /// <summary>
        ///     Compares two objects and generates the differences between them.
        /// </summary>
        /// <typeparam name="T">The type of the T.</typeparam>
        /// <param name="original">The original.</param>
        /// <param name="updated">The updated.</param>
        /// <returns>The result of the diff operation</returns>
        public static ObjectDiffPatchResult GenerateDiff<T>(T original, JObject updated) where T : class
        {
            // parse our objects
            var parsed = original != null ? JObject.FromObject(original, DefaultSerializer()) : null;

            // analyse their differences!
            return GenerateDiff(parsed, updated, typeof (T));
        }

        /// <summary>
        ///     Compares two objects and generates the differences between them.
        /// </summary>
        /// <typeparam name="T">The type of the T.</typeparam>
        /// <param name="original">The original.</param>
        /// <param name="updated">The updated.</param>
        /// <returns>The result of the diff operation</returns>
        public static ObjectDiffPatchResult GenerateDiff<T>(JObject original, T updated) where T : class
        {
            // parse our objects
            var parsed = updated != null ? JObject.FromObject(updated, DefaultSerializer()) : null;

            // analyse their differences!
            return GenerateDiff(original, parsed, typeof (T));
        }

        /// <summary>
        ///     Compares two objects and generates the differences between them.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <param name="updated">The updated.</param>
        /// <returns>The result of the diff operation</returns>
        public static ObjectDiffPatchResult GenerateDiff(JObject original, JObject updated)
        {
            // analyse their differences!
            return GenerateDiff(original, updated, typeof (JObject));
        }

        /// <summary>
        ///     Compares two objects and generates the differences between them.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <param name="updated">The updated.</param>
        /// <param name="comparedType">Type of the compared.</param>
        /// <returns>The result of the diff operation</returns>
        public static ObjectDiffPatchResult GenerateDiff(JObject original, JObject updated, Type comparedType)
        {
            // analyse their differences!
            var result = Diff(original, updated);
            result.Type = comparedType ?? typeof (JObject);

            return result;
        }

        /// <summary>
        ///     Modifies an object according to a diff, retuning a new object with applied patch.
        /// </summary>
        /// <typeparam name="T">The type of the T.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="diffJson">The diff json.</param>
        /// <returns>A new object with applied patch</returns>
        public static T PatchObject<T>(T source, string diffJson) where T : class
        {
            var diff = JObject.Parse(diffJson);

            return PatchObject(source, diff);
        }

        /// <summary>
        ///     Modifies an object according to a diff, retuning a new object with applied patch.
        /// </summary>
        /// <typeparam name="T">The type of the T.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="diffJson">The diff json.</param>
        /// <returns>A new object with applied patch</returns>
        public static T PatchObject<T>(T source, JObject diffJson) where T : class
        {
            var sourceJson = source != null ? JObject.FromObject(source, DefaultSerializer()) : null;
            var resultJson = Patch(sourceJson, diffJson);

            return resultJson?.ToObject<T>();
        }

        /// <summary>
        ///     Create an object snapshots as a Newtonsoft.Json.Linq.JObject.
        /// </summary>
        /// <typeparam name="T">The type of the T.</typeparam>
        /// <param name="source">The source object.</param>
        /// <returns>Newtonsoft.Json.Linq.JObject</returns>
        public static JObject Snapshot<T>(T source) where T : class
        {
            if (source == null) return null;

            if (typeof (JObject).IsAssignableFrom(typeof (T)))
            {
                var jObject = source as JObject;
                if (jObject != null) return (JObject) jObject.DeepClone();
            }

            return JObject.FromObject(source, DefaultSerializer());
        }

        private static ObjectDiffPatchResult Diff(JObject source, JObject target)
        {
            var result = new ObjectDiffPatchResult();
            // check for null values
            if (source == null && target == null)
            {
                return result;
            }
            if (source == null || target == null)
            {
                result.OldValues = source;
                result.NewValues = target;
                return result;
            }

            // compare internal fields           
            var removedNew = new JArray();
            var removedOld = new JArray();
            JToken token;
            // start by iterating in source fields
            foreach (var i in source)
            {
                // check if field exists
                if (!target.TryGetValue(i.Key, out token))
                {
                    AddOldValuesToken(result, i.Value, i.Key);
                    removedNew.Add(i.Key);
                }
                // compare field values
                else
                {
                    DiffField(i.Key, i.Value, token, result);
                }
            }
            // then iterate in target fields that are not present in source
            foreach (var i in target)
            {
                // ignore alredy compared values
                if (source.TryGetValue(i.Key, out token))
                    continue;
                // add missing tokens
                removedOld.Add(i.Key);
                AddNewValuesToken(result, i.Value, i.Key);
            }

            if (removedOld.Count > 0)
                AddOldValuesToken(result, removedOld, PREFIX_REMOVED_FIELDS);
            if (removedNew.Count > 0)
                AddNewValuesToken(result, removedNew, PREFIX_REMOVED_FIELDS);

            return result;
        }

        private static ObjectDiffPatchResult DiffField(string fieldName, JToken source, JToken target, ObjectDiffPatchResult result = null)
        {
            if (result == null) result = new ObjectDiffPatchResult();
            if (source == null)
            {
                if (target != null)
                {
                    AddToken(result, fieldName, null, target);
                }
            }
            else if (target == null)
            {
                AddToken(result, fieldName, source, null);
            }
            else if (source.Type == JTokenType.Object)
            {
                var v = target as JObject;
                var r = Diff(source as JObject, v);
                if (!r.AreEqual)
                    AddToken(result, fieldName, r);
            }
            else if (source.Type == JTokenType.Array)
            {
                var aS = source as JArray;
                var aT = target as JArray;

                if (aS != null && aT != null && (aS.Count == 0 || aT.Count == 0) && (aS.Count != aT.Count))
                {
                    AddToken(result, fieldName, source, target);
                }
                else
                {
                    var arrayDiff = new ObjectDiffPatchResult();

                    if (aS != null)
                    {
                        if (aT != null)
                        {
                            var minCount = Math.Min(aS.Count, aT.Count);
                            for (var i = 0; i < Math.Max(aS.Count, aT.Count); i++)
                            {
                                if (i < minCount)
                                {
                                    DiffField(i.ToString(), aS[i], aT[i], arrayDiff);
                                }
                                else if (i >= aS.Count)
                                {
                                    AddNewValuesToken(arrayDiff, aT[i], i.ToString());
                                }
                                else
                                {
                                    AddOldValuesToken(arrayDiff, aS[i], i.ToString());
                                }
                            }
                        }
                    }

                    if (!arrayDiff.AreEqual)
                    {
                        if (aT != null && aS != null && aS.Count != aT.Count)
                            AddToken(arrayDiff, PREFIX_ARRAY_SIZE, aS.Count, aT.Count);
                        AddToken(result, fieldName, arrayDiff);
                    }
                }
            }
            else
            {
                if (!JToken.DeepEquals(source, target))
                {
                    AddToken(result, fieldName, source, target);
                }
            }

            return result;
        }

        private static JsonSerializer InternalDefaultSerializer()
        {
            // create our custom serializer
            var writer = JsonSerializer.Create(DefaultSerializerSettings);

            return writer;
        }

        private static JsonSerializerSettings InternalDefaultSerializerSettings()
        {
            // ensure the serializer will not ignore null values
            JsonSerializerSettings settings = null;
            settings = JsonConvert.DefaultSettings != null ? JsonConvert.DefaultSettings() : new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Include;
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            settings.Formatting = Formatting.None;
            settings.MissingMemberHandling = MissingMemberHandling.Ignore;
            settings.ObjectCreationHandling = ObjectCreationHandling.Replace;

            return settings;
        }

        private static JToken Patch(JToken sourceJson, JToken diffJson)
        {
            JToken token;
            // deal with null values
            if (sourceJson == null || diffJson == null || !sourceJson.HasValues)
            {
                return diffJson;
            }
            if (diffJson.Type != JTokenType.Object)
            {
                return diffJson;
            }
            // deal with objects
            var diffObj = (JObject) diffJson;
            if (sourceJson.Type == JTokenType.Array)
            {
                var sz = 0;
                var foundArraySize = diffObj.TryGetValue(PREFIX_ARRAY_SIZE, out token);
                if (foundArraySize)
                {
                    diffObj.Remove(PREFIX_ARRAY_SIZE);
                    sz = token.Value<int>();
                }
                var array = sourceJson as JArray;
                // resize array
                if (array != null && foundArraySize && array.Count != sz)
                {
                    var snapshot = array.DeepClone() as JArray;
                    array.Clear();
                    for (var i = 0; i < sz; i++)
                    {
                        array.Add(snapshot != null && i < snapshot.Count ? snapshot[i] : null);
                    }
                }
                // patch it
                foreach (var f in diffObj)
                {
                    int ix;
                    if (int.TryParse(f.Key, out ix))
                    {
                        if (array != null) array[ix] = Patch(array[ix], f.Value);
                    }
                }
            }
            else
            {
                var sourceObj = sourceJson as JObject ?? new JObject();
                // remove fields
                if (diffObj.TryGetValue(PREFIX_REMOVED_FIELDS, out token))
                {
                    diffObj.Remove(PREFIX_REMOVED_FIELDS);
                    var jArray = token as JArray;
                    if (jArray != null)
                        foreach (var f in jArray)
                            sourceObj.Remove(f.ToString());
                }

                // patch it
                foreach (var f in diffObj)
                {
                    sourceObj[f.Key] = Patch(sourceObj[f.Key], f.Value);
                }
            }

            return sourceJson;
        }

        private static void AddNewValuesToken(ObjectDiffPatchResult item, JToken newToken, string fieldName)
        {
            if (item.NewValues == null)
                item.NewValues = new JObject();
            item.NewValues[fieldName] = newToken;
        }

        private static void AddOldValuesToken(ObjectDiffPatchResult item, JToken oldToken, string fieldName)
        {
            if (item.OldValues == null)
                item.OldValues = new JObject();
            item.OldValues[fieldName] = oldToken;
        }

        private static void AddToken(ObjectDiffPatchResult item, string fieldName, JToken oldToken, JToken newToken)
        {
            AddOldValuesToken(item, oldToken, fieldName);

            AddNewValuesToken(item, newToken, fieldName);
        }

        private static void AddToken(ObjectDiffPatchResult item, string fieldName, ObjectDiffPatchResult diff)
        {
            AddToken(item, fieldName, diff.OldValues, diff.NewValues);
        }
    }
}