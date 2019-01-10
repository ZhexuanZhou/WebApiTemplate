namespace TemplateDemo.Infrastrature.Database
{
    public static class RoleAndPolicy
    {
        public static class RoleName
        {
            public const string User = "User";
            public const string SuperUser = "SuperUser";
        }
        public static class PolicyName
        {
            public const string Users = "Users";
            public const string SuperUsers = "SuperUsers";
        }

        public static class ClaimName
        {
            public const string User = "User";
            public const string SuperUser = "SuperUser";
        }
    }
}