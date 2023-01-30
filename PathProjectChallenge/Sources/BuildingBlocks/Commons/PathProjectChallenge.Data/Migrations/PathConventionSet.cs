﻿using FluentMigrator.Runner.Conventions;
using FluentMigrator.Runner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathProjectChallenge.Data.Migrations
{
    /// <summary>
    /// A set conventions to be applied to expressions
    /// </summary>
    public class PathConventionSet : IConventionSet
    {
        #region Ctor

        public PathConventionSet(IPathDataProvider dataProvider)
        {
            if (dataProvider is null)
                throw new ArgumentNullException(nameof(dataProvider));

            var defaultConventionSet = new DefaultConventionSet();

            ForeignKeyConventions = new List<IForeignKeyConvention>()
            {
                new PathForeignKeyConvention(dataProvider),
                defaultConventionSet.SchemaConvention,
            };

            IndexConventions = new List<IIndexConvention>()
            {
                new PathIndexConvention(dataProvider),
                defaultConventionSet.SchemaConvention
            };

            ColumnsConventions = new List<IColumnsConvention>()
            {
                new PathColumnsConvention(),
                new DefaultPrimaryKeyNameConvention()
            };

            ConstraintConventions = defaultConventionSet.ConstraintConventions;

            SequenceConventions = defaultConventionSet.SequenceConventions;
            AutoNameConventions = defaultConventionSet.AutoNameConventions;
            SchemaConvention = defaultConventionSet.SchemaConvention;
            RootPathConvention = defaultConventionSet.RootPathConvention;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the root path convention to be applied to <see cref="T:FluentMigrator.Expressions.IFileSystemExpression" /> implementations
        /// </summary>
        public IRootPathConvention RootPathConvention { get; }

        /// <summary>
        /// Gets the default schema name convention to be applied to <see cref="T:FluentMigrator.Expressions.ISchemaExpression" /> implementations
        /// </summary>
        /// <remarks>
        /// This class cannot be overridden. The <see cref="T:FluentMigrator.Runner.Conventions.IDefaultSchemaNameConvention" />
        /// must be implemented/provided instead.
        /// </remarks>
        public DefaultSchemaConvention SchemaConvention { get; }

        /// <summary>
        /// Gets the conventions to be applied to <see cref="T:FluentMigrator.Expressions.IColumnsExpression" /> implementations
        /// </summary>
        public IList<IColumnsConvention> ColumnsConventions { get; }

        /// <summary>
        /// Gets the conventions to be applied to <see cref="T:FluentMigrator.Expressions.IConstraintExpression" /> implementations
        /// </summary>
        public IList<IConstraintConvention> ConstraintConventions { get; }

        /// <summary>
        /// Gets the conventions to be applied to <see cref="T:FluentMigrator.Expressions.IForeignKeyExpression" /> implementations
        /// </summary>
        public IList<IForeignKeyConvention> ForeignKeyConventions { get; }

        /// <summary>
        /// Gets the conventions to be applied to <see cref="T:FluentMigrator.Expressions.IIndexExpression" /> implementations
        /// </summary>
        public IList<IIndexConvention> IndexConventions { get; }

        /// <summary>
        /// Gets the conventions to be applied to <see cref="T:FluentMigrator.Expressions.ISequenceExpression" /> implementations
        /// </summary>
        public IList<ISequenceConvention> SequenceConventions { get; }

        /// <summary>
        /// Gets the conventions to be applied to <see cref="T:FluentMigrator.Expressions.IAutoNameExpression" /> implementations
        /// </summary>
        public IList<IAutoNameConvention> AutoNameConventions { get; }

        #endregion
    }
}
