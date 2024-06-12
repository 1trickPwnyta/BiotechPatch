using HarmonyLib;
using System.Xml;
using Verse;

namespace BiotechPatch
{
    public class PatchOperationReplaceIf : PatchOperationReplace
    {
        private XmlContainer setting;

        protected override bool ApplyWorker(XmlDocument xml)
        {
            string settingText = setting.node.InnerText;
            return (bool)typeof(BiotechPatchSettings).Field(settingText).GetValue(null) ? base.ApplyWorker(xml) : true;
        }
    }
}
