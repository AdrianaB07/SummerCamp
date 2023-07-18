function showDeleteConfirmation(teamId) {
    $('#deleteConfirmationModal').modal('show');
    $('#deleteButton').attr('href', '/Teams/Delete?teamId=' + teamId);
}