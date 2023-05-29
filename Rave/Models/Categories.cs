using System;
using System.Collections.Generic;
namespace Rave.Models
{

    internal class Categories
    {

        private List<Category> categories = new List<Category>();

        // Método para consultar la lista de categorías
        public List<Category> GetCategories()
        {
            return categories;
        }

        // Método para añadir una categoría a la lista
        public void AddCategory(Category categoria)
        {
            categories.Add(categoria);
        }

        // Método para eliminar una categoría de la lista
        public void RemoveCategory(Category categoria)
        {
            categories.Remove(categoria);
        }

        // Método para buscar una categoría por su nombre
        public Category GetCategory(string name)
        {
            foreach (Category category in categories)
            {
                if (category.Name.Equals(name))
                {
                    return category;
                }
            }
            return null;
        }
    }
}
