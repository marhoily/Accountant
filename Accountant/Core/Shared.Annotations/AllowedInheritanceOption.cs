namespace NewModel.Shared.Annotations
{
	public enum AllowedInheritanceOption
	{
        /// <summary>
        /// Anyone can inherit
        /// </summary>
		All,
        /// <summary>Some classes may inherit this class in dynamic code 
        /// (like EntityFramework, Mocks, etc.)
        /// but in our code this is prohibited </summary>
        DynamicOnly
	}
}