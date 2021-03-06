﻿using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DC=DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using IntecoAG.XpoExt;

namespace IntecoAG.XafExt.Spreadsheet.MultiDimForms.Core {
    //[DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    [Persistent("FmMdfCoreHierarchy")]
    [ModelDefault("IsCloneable", "True")]
    public class MdfCoreHierarchy : MdfCoreElement { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).

        private MdfCoreDomain _Domain;
        [Association("FmMdfCoreDomain-FmMdfCoreHierarchy")]
        [ExplicitLoading(1)]
        [DataSourceProperty(nameof(Container) + "." + nameof(MdfCoreContainer.Domains))]
        public MdfCoreDomain Domain {
            get { return _Domain; }
            set { SetPropertyValue<MdfCoreDomain>(ref _Domain, value); }
        }

        [PersistentAlias(nameof(Domain))]
        [VisibleInListView(false)]
        [DataSourceProperty(nameof(Container) + "." + nameof(MdfCoreContainer.Domains))]
        public MdfCoreDomain DomainUi {
            get { return Domain; }
            set { Domain = value; }
        }

        private MdfCoreContainer _Container;
        [Association("FmMdfContainer-FmMdfCoreHierarchy")]
        public MdfCoreContainer Container {
            get { return _Container; }
            set { SetPropertyValue<MdfCoreContainer>(ref _Container, value); }
        }

        private MdfCoreHierarchyNode _RootNode;
        [Aggregated]
        public MdfCoreHierarchyNode RootNode {
            get { return _RootNode; }
            set { SetPropertyValue(ref _RootNode, value); }
        }

        [VisibleInDetailView(false)]
        [Association("FmMdfCoreHierarchy-FmMdfCoreHierarchyNode")]
        [Aggregated]
        public XPCollection<MdfCoreHierarchyNode> Nodes {
            get { return GetCollection<MdfCoreHierarchyNode>(); }
        }

        public MdfCoreHierarchy(Session session) : base(session) {
        }
        public override void AfterConstruction() {
            base.AfterConstruction();
            RootNode = new MdfCoreHierarchyNode(Session);
            Nodes.Add(RootNode);
        }
        protected override void OnChanged(String property_name, Object old_value, Object new_value) {
            base.OnChanged(property_name, old_value, new_value);
            switch (property_name) {
                case nameof(Domain):
                    Container = Domain?.Container;
                    break;
                case nameof(Container):
                    if (!ReferenceEquals(Domain?.Container, Container))
                        Domain = null;
                    break;
            }
        }
        //public override string ToString() {
        //    return base.ToString();
        //}
        //private string _PersistentProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
        //public string PersistentProperty {
        //    get { return _PersistentProperty; }
        //    set { SetPropertyValue(ref _PersistentProperty, value); }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}

        //protected override void OnChanged(String property_name, Object old_value, Object new_value) {
        //    base.OnChanged(property_name, old_value, new_value);
        //}
    }
}