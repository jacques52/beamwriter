using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;

namespace beamWriter
{
    public class beamWriter : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public beamWriter()
          : base("beam writer", "Nickname",
              "Description",
              "Category", "Subcategory")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("pointSet", "pts", "pointset", GH_ParamAccess.list);
            pManager.AddIntegerParameter("index", "index", "indexOfDuplicates", GH_ParamAccess.tree);
            pManager.AddNumberParameter("radius", "index", "indexOfDuplicates", GH_ParamAccess.list);
            //pManager.AddBooleanParameter("toggle", "toggle", "booleanToggle", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("nodeSet", "string", "string", GH_ParamAccess.list);
            pManager.AddTextParameter("elementSet", "string", "string", GH_ParamAccess.tree);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {   

            //create some placeholders.
            //var ptList = new List<Point3d>();
            List<Point3d> ptList = new List<Point3d>();
            GH_Structure<GH_Integer> index = new GH_Structure<GH_Integer>();
            
            List<double> radius = new List<double>();


            //set the data
            DA.GetDataList(0, ptList);
            DA.GetDataTree(1, out index);
            DA.GetDataList(2, radius);


            // logic
            //node
            //string header = "*Node";
            List<string> node = new List<string>();

            //pt.ToString;
            int length = ptList.Count;
            List<string> ptString = new List<String>();
            
            ///Here we will format our string for the node class
            for (int i = 1; i < ptList.Count; i++)
            {

                node.Add( i.ToString()+ ",   " +  ptList[i].ToString());
            }


            //element
            List<string> element = new List<String>();
            //This is going to be our element string formatter
            List<int> indexItems = new List<int>(index.DataCount);

            for (int i = 0; i<index.Branches.Count; i++)
            {
                GH_Path pathTemp = index.get_Path(i);
                element.Add(i.ToString() + ",   " + index.get_DataItem(pathTemp, 0).ToString()+"," + index.get_DataItem(pathTemp, 1).ToString());
            }
               
            //This is going to be our eleset string formatter

            //This is our section string formatter



            DA.SetDataList(0, ptString);
            DA.SetDataList(1, element);
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {

                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{7a0cc4f1-5ea9-40b1-a907-2760385fbc16}"); }
        }
    }
}
