//-------------------------------------------------------------------------
// <copyright file="Recipe.cs" company="Universidad Católica del Uruguay">
// Copyright (c) Programación II. Derechos reservados.
// </copyright>
//-------------------------------------------------------------------------

using System;
using System.Collections;

namespace Full_GRASP_And_SOLID.Library
{
    public class Recipe
    {
        private ArrayList steps = new ArrayList();

        public Product FinalProduct { get; set; }

        public void AddStep(Step step)
        {
            this.steps.Add(step);
        }

        public void RemoveStep(Step step)
        {
            this.steps.Remove(step);
        }

        /// <summary>
        /// En este caso se utiliza expert para la asignación de responsabilidades; como la clase recipe
        /// tiene todos los datos necesarios para crear el método, se crea el mismo dentro de esa clase.
        /// De haber querido utilizar SRP, se tendría que separar PrintRecipe de la clase recipe,
        /// debido a que como ya tiene metodos para añadir y quitar elementos del ArrayList steps,
        /// ya tendría dos razones de cambio, una sería si por ejemplo se quiere cambiar el texto a imprimir,
        /// y otra sería si se quisiera cambiar la implementación de steps, por ejemplo cambiando la forma
        /// en la que se almacenan los datos.
        /// También se tendría que crear una nueva clase para el cálculo del costo de producción
        /// y enviarle todos los datos necesarios a la misma, lo que incluiría modificar el atributo steps
        /// para poder acceder a los datos desde fuera de recipe, y luego poder realizar
        /// la implementación del método pedido y enviar el costo de producción a la impresión de la receta.
        /// </summary>
        /// <returns></returns>

        public double GetProductionCost()
        {
            double sumaInsumos = 0;
            double costoEquipamiento = 0;
            double total = 0;
            foreach (Step step in this.steps)
            {
                // el costo de los insumos es el precio del insumo por la cantidad necesaria
                sumaInsumos = sumaInsumos + (step.Quantity * step.Input.UnitCost);
                costoEquipamiento = costoEquipamiento + (step.Equipment.HourlyCost * step.Time);
            }
            total = sumaInsumos + costoEquipamiento;
            return total;
        }

        public void PrintRecipe()
        {
            Console.WriteLine($"Receta de {this.FinalProduct.Description}:");
            foreach (Step step in this.steps)
            {
                Console.WriteLine($"{step.Quantity} de '{step.Input.Description}' " +
                    $"usando '{step.Equipment.Description}' durante {step.Time}");
            }
            Console.WriteLine($"costo total de producción: {GetProductionCost()}");
        }
    }
}