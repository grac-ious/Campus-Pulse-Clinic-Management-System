namespace Campus_pulse
{
    public static class Session
    {
        public static int UserID { get; set; }
        public static string Username { get; set; }
        public static string FullName { get; set; }
        public static string RoleName { get; set; }

        public static void Clear()
        {
            UserID = 0;
            Username = null;
            FullName = null;
            RoleName = null;
        }
    }
}
