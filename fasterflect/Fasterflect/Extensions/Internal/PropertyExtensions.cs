﻿#region License
// Copyright 2010 Buu Nguyen, Morten Mertner
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
// 
// The latest version of this file can be found at http://fasterflect.codeplex.com/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
namespace Fasterflect.Extensions
{
	/// <summary>
	/// Extension methods for locating and accessing properties.
	/// </summary>
	public static partial class PropertyExtensions
	{
		#region Property Access
		/// <summary>
		/// Sets the property specified by <param name="name"/> on the given <param name="obj"/> to the 
		/// specified <param name="value" />.
		/// </summary>
		/// <returns><paramref name="obj"/>.</returns>
		internal static object SetPropertyValue(this object obj, string name, object value)
		{
			Fasterflect.Extensions.PropertyExtensions.DelegateForSetPropertyValue(obj.GetTypeAdjusted(), name)(obj, value);
			return obj;
		}

		/// <summary>
		/// Gets the value of the property specified by <param name="name"/> on the given <param name="obj"/>.
		/// </summary>
		internal static object GetPropertyValue(this object obj, string name)
		{
			return Fasterflect.Extensions.PropertyExtensions.DelegateForGetPropertyValue(obj.GetTypeAdjusted(), name)(obj);
		}

		/// <summary>
		/// Sets the property specified by <param name="name"/> matching <param name="bindingFlags"/>
		/// on the given <param name="obj"/> to the specified <param name="value" />.
		/// </summary>
		/// <returns><paramref name="obj"/>.</returns>
		internal static object SetPropertyValue(this object obj, string name, object value, FasterflectFlags bindingFlags)
		{
			Fasterflect.Extensions.PropertyExtensions.DelegateForSetPropertyValue(obj.GetTypeAdjusted(), name, bindingFlags)(obj, value);
			return obj;
		}

		/// <summary>
		/// Gets the value of the property specified by <param name="name"/> matching <param name="bindingFlags"/>
		/// on the given <param name="obj"/>.
		/// </summary>
		internal static object GetPropertyValue(this object obj, string name, FasterflectFlags bindingFlags)
		{
			return Fasterflect.Extensions.PropertyExtensions.DelegateForGetPropertyValue(obj.GetTypeAdjusted(), name, bindingFlags)(obj);
		}

		/// <summary>
		/// Sets the property specified by <param name="memberExpression"/> on the given <param name="obj"/> to the 
		/// specified <param name="value" />.
		/// </summary>
		/// <returns><paramref name="obj"/>.</returns>
		internal static object SetPropertyValue(this object obj, Expression<Func<object>> memberExpression, object value)
		{
			MemberExpression body = memberExpression != null ? memberExpression.Body as MemberExpression : null;
			if (body == null || body.Member == null) {
				throw new ArgumentNullException(nameof(memberExpression));
			}
			return obj.SetPropertyValue(body.Member.Name, value);
		}

		/// <summary>
		/// Gets the value of the property specified by <param name="memberExpression"/> on the given <param name="obj"/>.
		/// </summary>
		internal static object GetPropertyValue(this object obj, Expression<Func<object>> memberExpression)
		{
			MemberExpression body = memberExpression != null ? memberExpression.Body as MemberExpression : null;
			if (body == null || body.Member == null) {
				throw new ArgumentNullException(nameof(memberExpression));
			}
			return obj.GetPropertyValue(body.Member.Name);
		}
		#endregion

		#region Indexer Access
		/// <summary>
		/// Sets the value of the indexer of the given <paramref name="obj"/>
		/// </summary>
		/// <param name="obj">The object whose indexer is to be set.</param>
		/// <param name="parameters">The list of the indexer parameters plus the value to be set to the indexer.
		/// The parameter types are determined from these parameters, therefore no parameter can be <c>null</c>.
		/// If any parameter is <c>null</c> (or you can't be sure of that, i.e. receive from a variable), 
		/// use a different overload of this method.</param>
		/// <returns>The object whose indexer is to be set.</returns>
		/// <example>
		/// If the indexer is of type <c>string</c> and accepts one parameter of type <c>int</c>, this 
		/// method should be invoked as follow:
		/// <code>
		/// obj.SetIndexer(new Type[]{typeof(int), typeof(string)}, new object[]{1, "a"});
		/// </code>
		/// </example>
		internal static object SetIndexer(this object obj, params object[] parameters)
		{
			Fasterflect.Extensions.PropertyExtensions.DelegateForSetIndexer(obj.GetTypeAdjusted(), parameters.ToTypeArray())(obj, parameters);
			return obj;
		}

		/// <summary>
		/// Sets the value of the indexer of the given <paramref name="obj"/>
		/// </summary>
		/// <param name="obj">The object whose indexer is to be set.</param>
		/// <param name="parameterTypes">The types of the indexer parameters (must be in the right order), plus
		/// the type of the indexer.</param>
		/// <param name="parameters">The list of the indexer parameters plus the value to be set to the indexer.
		/// This list must match with the <paramref name="parameterTypes"/> list.</param>
		/// <returns>The object whose indexer is to be set.</returns>
		/// <example>
		/// If the indexer is of type <c>string</c> and accepts one parameter of type <c>int</c>, this 
		/// method should be invoked as follow:
		/// <code>
		/// obj.SetIndexer(new Type[]{typeof(int), typeof(string)}, new object[]{1, "a"});
		/// </code>
		/// </example>
		internal static object SetIndexer(this object obj, Type[] parameterTypes, params object[] parameters)
		{
			Fasterflect.Extensions.PropertyExtensions.DelegateForSetIndexer(obj.GetTypeAdjusted(), parameterTypes)(obj, parameters);
			return obj;
		}

		/// <summary>
		/// Gets the value of the indexer of the given <paramref name="obj"/>
		/// </summary>
		/// <param name="obj">The object whose indexer is to be retrieved.</param>
		/// <param name="parameters">The list of the indexer parameters.
		/// The parameter types are determined from these parameters, therefore no parameter can be <code>null</code>.
		/// If any parameter is <code>null</code> (or you can't be sure of that, i.e. receive from a variable), 
		/// use a different overload of this method.</param>
		/// <returns>The value returned by the indexer.</returns>
		internal static object GetIndexer(this object obj, params object[] parameters)
		{
			return Fasterflect.Extensions.PropertyExtensions.DelegateForGetIndexer(obj.GetTypeAdjusted(), parameters.ToTypeArray())(obj, parameters);
		}

		/// <summary>
		/// Gets the value of the indexer of the given <paramref name="obj"/>
		/// </summary>
		/// <param name="obj">The object whose indexer is to be retrieved.</param>
		/// <param name="parameterTypes">The types of the indexer parameters (must be in the right order).</param>
		/// <param name="parameters">The list of the indexer parameters.</param>
		/// <returns>The value returned by the indexer.</returns>
		internal static object GetIndexer(this object obj, Type[] parameterTypes, params object[] parameters)
		{
			return Fasterflect.Extensions.PropertyExtensions.DelegateForGetIndexer(obj.GetTypeAdjusted(), parameterTypes)(obj, parameters);
		}

		/// <summary>
		/// Sets the value of the indexer matching <paramref name="bindingFlags"/> of the given <paramref name="obj"/>
		/// </summary>
		/// <param name="obj">The object whose indexer is to be set.</param>
		/// <param name="bindingFlags">The binding flags used to lookup the indexer.</param>
		/// <param name="parameters">The list of the indexer parameters plus the value to be set to the indexer.
		/// The parameter types are determined from these parameters, therefore no parameter can be <c>null</c>.
		/// If any parameter is <c>null</c> (or you can't be sure of that, i.e. receive from a variable), 
		/// use a different overload of this method.</param>
		/// <returns>The object whose indexer is to be set.</returns>
		/// <example>
		/// If the indexer is of type <c>string</c> and accepts one parameter of type <c>int</c>, this 
		/// method should be invoked as follow:
		/// <code>
		/// obj.SetIndexer(new Type[]{typeof(int), typeof(string)}, new object[]{1, "a"});
		/// </code>
		/// </example>
		internal static object SetIndexer(this object obj, FasterflectFlags bindingFlags, params object[] parameters)
		{
			Fasterflect.Extensions.PropertyExtensions.DelegateForSetIndexer(obj.GetTypeAdjusted(), bindingFlags, parameters.ToTypeArray())(obj,
																										parameters);
			return obj;
		}

		/// <summary>
		/// Sets the value of the indexer matching <paramref name="bindingFlags"/> of the given <paramref name="obj"/>
		/// </summary>
		/// <param name="obj">The object whose indexer is to be set.</param>
		/// <param name="parameterTypes">The types of the indexer parameters (must be in the right order), plus
		///   the type of the indexer.</param>
		/// <param name="bindingFlags">The binding flags used to lookup the indexer.</param>
		/// <param name="parameters">The list of the indexer parameters plus the value to be set to the indexer.
		///   This list must match with the <paramref name="parameterTypes"/> list.</param>
		/// <returns>The object whose indexer is to be set.</returns>
		/// <example>
		/// If the indexer is of type <c>string</c> and accepts one parameter of type <c>int</c>, this 
		/// method should be invoked as follow:
		/// <code>
		/// obj.SetIndexer(new Type[]{typeof(int), typeof(string)}, new object[]{1, "a"});
		/// </code>
		/// </example>
		internal static object SetIndexer(this object obj, Type[] parameterTypes, FasterflectFlags bindingFlags, params object[] parameters)
		{
			Fasterflect.Extensions.PropertyExtensions.DelegateForSetIndexer(obj.GetTypeAdjusted(), bindingFlags, parameterTypes)(obj, parameters);
			return obj;
		}

		/// <summary>
		/// Gets the value of the indexer matching <paramref name="bindingFlags"/> of the given <paramref name="obj"/>
		/// </summary>
		/// <param name="obj">The object whose indexer is to be retrieved.</param>
		/// <param name="bindingFlags">The binding flags used to lookup the indexer.</param>
		/// <param name="parameters">The list of the indexer parameters.
		/// The parameter types are determined from these parameters, therefore no parameter can be <code>null</code>.
		/// If any parameter is <code>null</code> (or you can't be sure of that, i.e. receive from a variable), 
		/// use a different overload of this method.</param>
		/// <returns>The value returned by the indexer.</returns>
		internal static object GetIndexer(this object obj, FasterflectFlags bindingFlags, params object[] parameters)
		{
			return Fasterflect.Extensions.PropertyExtensions.DelegateForGetIndexer(obj.GetTypeAdjusted(), bindingFlags, parameters.ToTypeArray())(obj,
																											   parameters);
		}

		/// <summary>
		/// Gets the value of the indexer matching <paramref name="bindingFlags"/> of the given <paramref name="obj"/>
		/// </summary>
		/// <param name="obj">The object whose indexer is to be retrieved.</param>
		/// <param name="parameterTypes">The types of the indexer parameters (must be in the right order).</param>
		/// <param name="bindingFlags">The binding flags used to lookup the indexer.</param>
		/// <param name="parameters">The list of the indexer parameters.</param>
		/// <returns>The value returned by the indexer.</returns>
		internal static object GetIndexer(this object obj, Type[] parameterTypes, FasterflectFlags bindingFlags, params object[] parameters)
		{
			return Fasterflect.Extensions.PropertyExtensions.DelegateForGetIndexer(obj.GetTypeAdjusted(), bindingFlags, parameterTypes)(obj, parameters);
		}
		#endregion

		#region Property Combined

		#region TryGetValue
		/// <summary>
		/// Gets the first (public or non-public) instance property with the given <paramref name="name"/> on the given
		/// <paramref name="obj"/> object. Returns the value of the property if a match was found and null otherwise.
		/// </summary>
		/// <remarks>
		/// When using this method it is not possible to distinguish between a missing property and a property whose value is null.
		/// </remarks>
		/// <param name="obj">The source object on which to find the property</param>
		/// <param name="name">The name of the property whose value should be retrieved</param>
		/// <returns>The value of the property or null if no property was found</returns>
		internal static object TryGetPropertyValue(this object obj, string name)
		{
			return TryGetPropertyValue(obj, name, FasterflectFlags.InstanceAnyVisibility);
		}

		/// <summary>
		/// Gets the first property with the given <paramref name="name"/> on the given <paramref name="obj"/> object.
		/// Returns the value of the property if a match was found and null otherwise.
		/// Use the <paramref name="bindingFlags"/> parameter to limit the scope of the search.
		/// </summary>
		/// <remarks>
		/// When using this method it is not possible to distinguish between a missing property and a property whose value is null.
		/// </remarks>
		/// <param name="obj">The source object on which to find the property</param>
		/// <param name="name">The name of the property whose value should be retrieved</param>
		/// <param name="bindingFlags">A combination of Flags that define the scope of the search</param>
		/// <returns>The value of the property or null if no property was found</returns>
		internal static object TryGetPropertyValue(this object obj, string name, FasterflectFlags bindingFlags)
		{
			try {
				return obj.GetPropertyValue(name, bindingFlags);
			}
			catch (MissingMemberException) {
				return null;
			}
		}
		#endregion

		#region TrySetValue
		/// <summary>
		/// Sets the first (public or non-public) instance property with the given <paramref name="name"/> on the 
		/// given <paramref name="obj"/> object to the supplied <paramref name="value"/>. Returns true 
		/// if a value was assigned to a property and false otherwise.
		/// </summary>
		/// <param name="obj">The source object on which to find the property</param>
		/// <param name="name">The name of the property whose value should be retrieved</param>
		/// <param name="value">The value that should be assigned to the property</param>
		/// <returns>True if the value was assigned to a property and false otherwise</returns>
		internal static bool TrySetPropertyValue(this object obj, string name, object value)
		{
			return TrySetPropertyValue(obj, name, value, FasterflectFlags.InstanceAnyVisibility);
		}

		/// <summary>
		/// Sets the first property with the given <paramref name="name"/> on the given <paramref name="obj"/> object
		/// to the supplied <paramref name="value"/>. Returns true if a value was assigned to a property and false otherwise.
		/// Use the <paramref name="bindingFlags"/> parameter to limit the scope of the search.
		/// </summary>
		/// <param name="obj">The source object on which to find the property</param>
		/// <param name="name">The name of the property whose value should be retrieved</param>
		/// <param name="value">The value that should be assigned to the property</param>
		/// <param name="bindingFlags">A combination of Flags that define the scope of the search</param>
		/// <returns>True if the value was assigned to a property and false otherwise</returns>
		internal static bool TrySetPropertyValue(this object obj, string name, object value, FasterflectFlags bindingFlags)
		{
			try {
				obj.SetPropertyValue(name, value, bindingFlags);
				return true;
			}
			catch (MissingMemberException) {
				return false;
			}
		}
		#endregion

		#endregion
	}
}
