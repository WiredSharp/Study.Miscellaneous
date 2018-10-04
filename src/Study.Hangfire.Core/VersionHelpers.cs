using System;
using System.Reflection;

namespace Study.Hangfire
{
	public static class VersionHelpers
	{
		public static int GetApiVersion(this Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException(nameof(assembly), "cannot resolve api version, no assembly provided");
			}

			AssemblyInformationalVersionAttribute attribute = (AssemblyInformationalVersionAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyInformationalVersionAttribute));
			if (!Int32.TryParse(attribute?.InformationalVersion?.Split('.')[0], out int apiVersion))
			{
				apiVersion = 1;
			}
			return apiVersion;
		}
	}
}
