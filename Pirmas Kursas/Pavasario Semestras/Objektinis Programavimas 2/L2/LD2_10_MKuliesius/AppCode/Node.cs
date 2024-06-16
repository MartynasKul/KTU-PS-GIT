namespace LD2_10_MKuliesius.AppCode
{
    public class Node<T>
    {
        #region private variables 
        //Aprašomi privatūs Node kintamieji
        private T data;
        private Node<T> next;

        #endregion
        #region Contructors
        /// <summary>
        /// Node konstruktorius
        /// </summary>
        /// <param name="data"> Node duomenys</param>
        /// <param name="next"> Sekantis Node</param>
        public Node(T data, Node<T> next) 
        {
            this.data = data;
            this.next = next;
        }

        #endregion
        #region Properties
        /// <summary>
        /// Data property
        /// </summary>
        public T Data 
        {
            get { return this.data; }
            set { this.data = value; }
        
        }

        /// <summary>
        /// Sekančio Node Property
        /// </summary>
        public Node<T> Next 
        {
            get { return this.next; }
            set { this.next = value; }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Gražina Node informaciją string forma
        /// </summary>
        public override string ToString()
        {
            return data.ToString();
        }
        #endregion
    }
}