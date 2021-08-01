
namespace LS.Localiser.CSV
{
    [System.Serializable]
    public class LocalizedString
    {

        #region Public Fields
        public string key;
        #endregion


        #region Private Fields
        #endregion


        #region Accessors
        public string value
        {
            get
            {
                return LocalizationSystem.GetLocalisedValue(key);
            }
        }
        #endregion


        #region Private Methods
        #endregion


        #region Public Methods
        public LocalizedString(string key)
        {
            this.key = key;
        }

        public static implicit operator LocalizedString(string key)
        {
            return new LocalizedString(key);
        }
        #endregion
    }
}