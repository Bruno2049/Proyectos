using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Collections;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Media;

namespace Restaurant
{

    // if the GridView exposed any methods at all that allowed for overriding at a control level, I would be
    // able to do all of this work inside it rather than the ListView. However, b/c it doesn't, I have to do the 
    // work inside the ListView.

    // The GridView has access to the ItemSource on the ListView through the dependency property mechanism.

    public class SortableListView : ListView
    {
        // The current column that is sorted.
        private SortListViewColumn sortColumn;

        // The previous column that was sorted.
        private SortListViewColumn previousSortColumn;

        // The current direction the header is sorted;
        private ListSortDirection sortDirection;

        protected override void OnInitialized(EventArgs e)
        {
            // Handle the event when a header is clicked.
            this.AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(OnHeaderClicked));
            base.OnInitialized(e);
        }

        /// <summary>
        /// A header was clicked. Sort the associated column.
        /// </summary>
        private void OnHeaderClicked(object sender, RoutedEventArgs e)
        {
            // Make sure the column is really being sorted.
            GridViewColumnHeader header = e.OriginalSource as GridViewColumnHeader;
            if (header == null || header.Role == GridViewColumnHeaderRole.Padding)
                return;

            SortListViewColumn column = header.Column as SortListViewColumn;
            if (column == null)
                return;

            // See if a new column was clicked, or the same column was clicked.
            if (sortColumn != column)
            {
                // A new column was clicked.
                previousSortColumn = sortColumn;
                sortColumn = column;
                sortDirection = ListSortDirection.Ascending;
            }
            else
            {
                // The same column was clicked, change the sort order.
                previousSortColumn = null;
                sortDirection = (sortDirection == ListSortDirection.Ascending) ?
                    ListSortDirection.Descending : ListSortDirection.Ascending;
            }

            // Sort the data.
            SortList(column.SortProperty);

            // Update the column header based on the sort column and order.
            UpdateHeaderTemplate();
        }

        /// <summary>
        /// Sort the data.
        /// </summary>
        private void SortList(string propertyName)
        {
            try
            {
                // Get the data to sort.
                ICollectionView dataView = CollectionViewSource.GetDefaultView(this.ItemsSource);

                // Specify the new sorting information.
                dataView.SortDescriptions.Clear();
                SortDescription description = new SortDescription(propertyName, sortDirection);
                dataView.SortDescriptions.Add(description);

                dataView.Refresh();
            }
            catch { }
        }

        /// <summary>
        /// Update the column header based on the sort column and order.
        /// </summary>
        private void UpdateHeaderTemplate()
        {
            Style headerStyle;

            // Restore the previous header.
            if (previousSortColumn != null && previousSortColumn.SortStyle != null)
            {
                headerStyle = this.TryFindResource(previousSortColumn.SortStyle) as Style;
                if (headerStyle != null)
                    previousSortColumn.HeaderContainerStyle = headerStyle;
            }

            // Update the current header.
            if (sortColumn.SortStyle != null)
            {
                // The name of the resource to use for the header.
                string resourceName = sortColumn.SortStyle + sortDirection.ToString();

                headerStyle = this.TryFindResource(resourceName) as Style;
                if (headerStyle != null)
                    sortColumn.HeaderContainerStyle = headerStyle;
            }
        }
    }
}
