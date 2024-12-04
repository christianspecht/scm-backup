/*
    * Add by ISC. Gicel Cordoba Pech. 
    Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
    Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
*/

namespace ScmBackup.Hosters
{
    internal class HosterProject
    {
        public HosterProject( string fullName, string key )
        {
            SetFullName( fullName );
            this.Key = key;
        }

        public void SetFullName(string name)
        {
            this.FullName = name.Replace( ' ', '#' );
        }

        public string FullName { get; private set; }

        public string Key { get; private set; }
        public bool IsPrivate { get; set; }

        public void SetPrivate( bool isPrivate ) {

            this.IsPrivate = isPrivate;
        }
    }
}