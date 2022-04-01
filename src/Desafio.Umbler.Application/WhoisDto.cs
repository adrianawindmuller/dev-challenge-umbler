namespace Desafio.Umbler.Application
{
    public class WhoisDto
    {
        public WhoisDto(string ip, string raw, int ttl, string organizationName)
        {
            IP = ip;
            Raw = raw;
            Ttl = ttl;
            OrganizationName = organizationName;
            Sucess = true;
        }

        public WhoisDto(string raw)
        {
            Raw = raw;
            Sucess = false;
        }


        public string IP { get; }

        public string Raw { get; }

        public int Ttl { get; }

        public string OrganizationName { get; }

        public bool Sucess { get; }
    }
}
