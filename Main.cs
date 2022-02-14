using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITraining_2022_2._4
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ICollection<Element> levels = collector.OfClass(typeof(Level)).ToElements();
            var query = from element in collector where element.Name == "Level 1" select element;

            List<Element> level1 = query.ToList<Element>();
            ElementId levelId = level1[0].Id;
            ElementLevelFilter level1Filter = new ElementLevelFilter(levelId,false);
            collector = new FilteredElementCollector(doc);
            ICollection<Element> allVentsOnLevel1 = collector.OfClass(typeof(MEPCurve)).WherePasses(level1Filter).ToElements();

            TaskDialog.Show("Количество воздуховодов на 1 этаже", allVentsOnLevel1.Count.ToString());

            List<Element> level2 = query.ToList<Element>();
            ElementId levelId2 = level2[0].Id;
            ElementLevelFilter level2Filter = new ElementLevelFilter(levelId, false);
            collector = new FilteredElementCollector(doc);
            ICollection<Element> allVentsOnLevel2 = collector.OfClass(typeof(MEPCurve)).WherePasses(level1Filter).ToElements();

            TaskDialog.Show("Количество воздуховодов на 2 этаже", allVentsOnLevel1.Count.ToString());

            return Result.Succeeded;
        }
    }
}
