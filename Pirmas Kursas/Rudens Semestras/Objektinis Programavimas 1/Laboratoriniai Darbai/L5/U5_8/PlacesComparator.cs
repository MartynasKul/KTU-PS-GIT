namespace U5_8
{
    class PlacesComparator
    {
        public virtual int Compare(Place a, Place b)
        {
            return a.CompareTo(b);
        }
    }
}
