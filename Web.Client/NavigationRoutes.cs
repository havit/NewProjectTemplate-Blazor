namespace Havit.NewProjectTemplate.Web.Client;

public static class NavigationRoutes
{
	public const string Home = "/";

	public static class Administration
	{
		public const string Index = "/admin/";
	}

	public static class UserAdministration
	{
		public const string PageName = "/admin/user/page-name";
	}

	public static class Diagnostics
	{
		public const string Info = "/diag/info";
	}

	public static class Errors
	{
		public const string AccessDenied = "/access-denied";
	}
}
