using System.Xml.Serialization;

namespace BootcampCoreServices.Model

{
    [XmlType("request")]
    public class Request
    {       
        [XmlElement("clientId", IsNullable = false)]
        public string ClientId { get; set; }

        [XmlElement("requestId", IsNullable = false)]
        public long RequestId { get; set; }

        [XmlElement("name", IsNullable = false)]
        public string Name { get; set; }

        [XmlElement("quantity", IsNullable = false)]
        public int Quantity { get; set; }

        [XmlElement("price", IsNullable = false)]
        public double Price { get; set; }

        public override string ToString() => ClientId + "\t " + RequestId + "\t " + Name + "\t " + Quantity + "\t " + Price;
    }
}
