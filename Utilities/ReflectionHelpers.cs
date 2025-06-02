using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TurboItems
{
	// Token: 0x02000029 RID: 41
	public static class ReflectionHelpers
	{
		// Token: 0x060001AD RID: 429 RVA: 0x0001D12C File Offset: 0x0001B32C
		public static IList CreateDynamicList(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type", "Argument cannot be null.");
			}
			foreach (ConstructorInfo constructorInfo in typeof(List<>).MakeGenericType(new Type[]
			{
				type
			}).GetConstructors())
			{
				if (constructorInfo.GetParameters().Length == 0)
				{
					return (IList)constructorInfo.Invoke(null, null);
				}
			}
			throw new ApplicationException("Could not create a new list with type <" + type.ToString() + ">.");
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0001D1B4 File Offset: 0x0001B3B4
		public static IDictionary CreateDynamicDictionary(Type typeKey, Type typeValue)
		{
			if (typeKey == null)
			{
				throw new ArgumentNullException("type_key", "Argument cannot be null.");
			}
			if (typeValue == null)
			{
				throw new ArgumentNullException("type_value", "Argument cannot be null.");
			}
			foreach (ConstructorInfo constructorInfo in typeof(Dictionary<,>).MakeGenericType(new Type[]
			{
				typeKey,
				typeValue
			}).GetConstructors())
			{
				if (constructorInfo.GetParameters().Length == 0)
				{
					return (IDictionary)constructorInfo.Invoke(null, null);
				}
			}
			throw new ApplicationException(string.Concat(new string[]
			{
				"Could not create a new dictionary with types <",
				typeKey.ToString(),
				",",
				typeValue.ToString(),
				">."
			}));
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0001D276 File Offset: 0x0001B476
		public static T ReflectGetField<T>(Type classType, string fieldName, object o = null)
		{
			return (T)((object)classType.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | ((o != null) ? BindingFlags.Instance : BindingFlags.Static)).GetValue(o));
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0001D294 File Offset: 0x0001B494
		public static void ReflectSetField<T>(Type classType, string fieldName, T value, object o = null)
		{
			classType.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | ((o != null) ? BindingFlags.Instance : BindingFlags.Static)).SetValue(o, value);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0001D2B3 File Offset: 0x0001B4B3
		public static T ReflectGetProperty<T>(Type classType, string propName, object o = null, object[] indexes = null)
		{
			return (T)((object)classType.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | ((o != null) ? BindingFlags.Instance : BindingFlags.Static)).GetValue(o, indexes));
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0001D2D2 File Offset: 0x0001B4D2
		public static void ReflectSetProperty<T>(Type classType, string propName, T value, object o = null, object[] indexes = null)
		{
			classType.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | ((o != null) ? BindingFlags.Instance : BindingFlags.Static)).SetValue(o, value, indexes);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0001D2F3 File Offset: 0x0001B4F3
		public static MethodInfo ReflectGetMethod(Type classType, string methodName, Type[] methodArgumentTypes = null, Type[] genericMethodTypes = null, bool? isStatic = null)
		{
			MethodInfo[] array = ReflectionHelpers.ReflectTryGetMethods(classType, methodName, methodArgumentTypes, genericMethodTypes, isStatic);
			if (array.Count<MethodInfo>() == 0)
			{
				throw new MissingMethodException("Cannot reflect method, not found based on input parameters.");
			}
			if (array.Count<MethodInfo>() > 1)
			{
				throw new InvalidOperationException("Cannot reflect method, more than one method matched based on input parameters.");
			}
			return array[0];
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0001D330 File Offset: 0x0001B530
		public static MethodInfo ReflectTryGetMethod(Type classType, string methodName, Type[] methodArgumentTypes = null, Type[] genericMethodTypes = null, bool? isStatic = null)
		{
			MethodInfo[] array = ReflectionHelpers.ReflectTryGetMethods(classType, methodName, methodArgumentTypes, genericMethodTypes, isStatic);
			MethodInfo result;
			if (array.Count<MethodInfo>() == 0)
			{
				result = null;
			}
			else if (array.Count<MethodInfo>() > 1)
			{
				result = null;
			}
			else
			{
				result = array[0];
			}
			return result;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0001D36C File Offset: 0x0001B56C
		public static MethodInfo[] ReflectTryGetMethods(Type classType, string methodName, Type[] methodArgumentTypes = null, Type[] genericMethodTypes = null, bool? isStatic = null)
		{
			BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic;
			if (isStatic == null || isStatic.Value)
			{
				bindingFlags |= BindingFlags.Static;
			}
			if (isStatic == null || !isStatic.Value)
			{
				bindingFlags |= BindingFlags.Instance;
			}
			MethodInfo[] methods = classType.GetMethods(bindingFlags);
			List<MethodInfo> list = new List<MethodInfo>();
			for (int i = 0; i < methods.Length; i++)
			{
				if (!(methods[i].Name != methodName))
				{
					if (methods[i].IsGenericMethodDefinition)
					{
						if (genericMethodTypes == null || genericMethodTypes.Length == 0 || methods[i].GetGenericArguments().Length != genericMethodTypes.Length)
						{
							goto IL_F5;
						}
						methods[i] = methods[i].MakeGenericMethod(genericMethodTypes);
					}
					else if (genericMethodTypes != null && genericMethodTypes.Length != 0)
					{
						goto IL_F5;
					}
					ParameterInfo[] parameters = methods[i].GetParameters();
					if (methodArgumentTypes != null)
					{
						if (parameters.Length != methodArgumentTypes.Length)
						{
							goto IL_F5;
						}
						for (int j = 0; j < parameters.Length; j++)
						{
							if (parameters[j].ParameterType != methodArgumentTypes[j])
							{
								goto IL_F5;
							}
						}
					}
					list.Add(methods[i]);
				}
			IL_F5:;
			}
			return list.ToArray();
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0001D484 File Offset: 0x0001B684
		public static object InvokeRefs<T0>(MethodInfo methodInfo, object o, T0 p0)
		{
			object[] parameters = new object[]
			{
				p0
			};
			return methodInfo.Invoke(o, parameters);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0001D4AC File Offset: 0x0001B6AC
		public static object InvokeRefs<T0>(MethodInfo methodInfo, object o, ref T0 p0)
		{
			object[] array = new object[]
			{
				p0
			};
			object result = methodInfo.Invoke(o, array);
			p0 = (T0)((object)array[0]);
			return result;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0001D4E4 File Offset: 0x0001B6E4
		public static object InvokeRefs<T0, T1>(MethodInfo methodInfo, object o, T0 p0, T1 p1)
		{
			object[] parameters = new object[]
			{
				p0,
				p1
			};
			return methodInfo.Invoke(o, parameters);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0001D514 File Offset: 0x0001B714
		public static object InvokeRefs<T0, T1>(MethodInfo methodInfo, object o, ref T0 p0, T1 p1)
		{
			object[] array = new object[]
			{
				p0,
				p1
			};
			object result = methodInfo.Invoke(o, array);
			p0 = (T0)((object)array[0]);
			return result;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0001D558 File Offset: 0x0001B758
		public static object InvokeRefs<T0, T1>(MethodInfo methodInfo, object o, T0 p0, ref T1 p1)
		{
			object[] array = new object[]
			{
				p0,
				p1
			};
			object result = methodInfo.Invoke(o, array);
			p1 = (T1)((object)array[1]);
			return result;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0001D59C File Offset: 0x0001B79C
		public static object InvokeRefs<T0, T1>(MethodInfo methodInfo, object o, ref T0 p0, ref T1 p1)
		{
			object[] array = new object[]
			{
				p0,
				p1
			};
			object result = methodInfo.Invoke(o, array);
			p0 = (T0)((object)array[0]);
			p1 = (T1)((object)array[1]);
			return result;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0001D5F0 File Offset: 0x0001B7F0
		public static object InvokeRefs<T0, T1, T2>(MethodInfo methodInfo, object o, T0 p0, T1 p1, T2 p2)
		{
			object[] parameters = new object[]
			{
				p0,
				p1,
				p2
			};
			return methodInfo.Invoke(o, parameters);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0001D628 File Offset: 0x0001B828
		public static object InvokeRefs<T0, T1, T2>(MethodInfo methodInfo, object o, ref T0 p0, T1 p1, T2 p2)
		{
			object[] array = new object[]
			{
				p0,
				p1,
				p2
			};
			object result = methodInfo.Invoke(o, array);
			p0 = (T0)((object)array[0]);
			return result;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0001D674 File Offset: 0x0001B874
		public static object InvokeRefs<T0, T1, T2>(MethodInfo methodInfo, object o, T0 p0, ref T1 p1, T2 p2)
		{
			object[] array = new object[]
			{
				p0,
				p1,
				p2
			};
			object result = methodInfo.Invoke(o, array);
			p1 = (T1)((object)array[1]);
			return result;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0001D6C0 File Offset: 0x0001B8C0
		public static object InvokeRefs<T0, T1, T2>(MethodInfo methodInfo, object o, T0 p0, T1 p1, ref T2 p2)
		{
			object[] array = new object[]
			{
				p0,
				p1,
				p2
			};
			object result = methodInfo.Invoke(o, array);
			p2 = (T2)((object)array[2]);
			return result;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0001D70C File Offset: 0x0001B90C
		public static object InvokeRefs<T0, T1, T2>(MethodInfo methodInfo, object o, ref T0 p0, ref T1 p1, T2 p2)
		{
			object[] array = new object[]
			{
				p0,
				p1,
				p2
			};
			object result = methodInfo.Invoke(o, array);
			p0 = (T0)((object)array[0]);
			p1 = (T1)((object)array[1]);
			return result;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0001D76C File Offset: 0x0001B96C
		public static object InvokeRefs<T0, T1, T2>(MethodInfo methodInfo, object o, ref T0 p0, T1 p1, ref T2 p2)
		{
			object[] array = new object[]
			{
				p0,
				p1,
				p2
			};
			object result = methodInfo.Invoke(o, array);
			p0 = (T0)((object)array[0]);
			p2 = (T2)((object)array[2]);
			return result;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0001D7CC File Offset: 0x0001B9CC
		public static object InvokeRefs<T0, T1, T2>(MethodInfo methodInfo, object o, T0 p0, ref T1 p1, ref T2 p2)
		{
			object[] array = new object[]
			{
				p0,
				p1,
				p2
			};
			object result = methodInfo.Invoke(o, array);
			p1 = (T1)((object)array[1]);
			p2 = (T2)((object)array[2]);
			return result;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0001D82C File Offset: 0x0001BA2C
		public static object InvokeRefs<T0, T1, T2>(MethodInfo methodInfo, object o, ref T0 p0, ref T1 p1, ref T2 p2)
		{
			object[] array = new object[]
			{
				p0,
				p1,
				p2
			};
			object result = methodInfo.Invoke(o, array);
			p0 = (T0)((object)array[0]);
			p1 = (T1)((object)array[1]);
			p2 = (T2)((object)array[2]);
			return result;
		}
	}
}
