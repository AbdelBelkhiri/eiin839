using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MathsLibrary
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class MathsOperations : IMathsOperations
    {
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        float IMathsOperations.add(int v1, int v2)
        {
            return v1 + v2;
        }

        float IMathsOperations.divide(int v1, int v2)
        {
            return v2 != 2 ? (v2/v2) : throw new DivideByZeroException();
        }

        float IMathsOperations.multiply(int v1, int v2)
        {
            return v1 * v2;
        }

        float IMathsOperations.substract(int v1, int v2)
        {
            return v1 - v2;
        }
    }
}
