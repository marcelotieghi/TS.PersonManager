// Inicialização da DataTable
$('#personTable').DataTable({
    responsive: true,
    lengthChange: false
});

// Handler para modal de criar/editar
$('body').on('click', '[data-bs-target="#personModal"]', async function () {
    const personId = $(this).data('id');
    const url = personId ? `/Person/Edit/${personId}` : '/Person/Create';
    const modalTitle = personId ? 'Edit Person' : 'Add New Person';

    try {
        const response = await $.get(url);
        $('#personModalBody').html(response);
        $('#personModalLabel').text(modalTitle);
        $('#personModal').modal('show');
    } catch (error) {
        console.error('Error loading modal:', error);
        alert('Failed to load form');
    }
});

// Handler para submit do formulário
$('body').on('submit', '#personForm', async function (e) {
    e.preventDefault();

    const formData = new FormData(this);
    const personId = $('#Id').val();
    const url = personId ? '/Person/Update' : '/Person/Create';

    try {
        await $.ajax({
            url: url,
            method: "POST",
            data: formData,
            processData: false,
            contentType: false
        });
        location.reload();
    } catch (error) {
        console.error('Save error:', error);
        alert("Error saving: " + error.responseText);
    }
});

// Handler para modal de detalhes
$('body').on('click', '.open-details-modal', async function () {
    const personId = $(this).data('id');

    try {
        const response = await $.get(`/Person/Details/${personId}`);
        $('#personDetailsModalBody').html(response);
        $('#personDetailsModal').modal('show');
    } catch (error) {
        console.error('Details error:', error);
        alert('Failed to load details');
    }
});

// Handler para deleção
$('body').on('click', '.delete-person', function () {
    const personId = $(this).data('id');
    $('#confirmDeleteBtn').data('person-id', personId);
    $('#confirmDeleteModal').modal('show');
});

$('#confirmDeleteBtn').on('click', async function () {
    const personId = $(this).data('person-id');

    try {
        await $.ajax({
            url: `/Person/Delete/${personId}`,
            method: 'POST'
        });
        $('#confirmDeleteModal').modal('hide');
        location.reload();
    } catch (error) {
        console.error('Delete error:', error);
        alert("Delete failed: " + error.responseText);
    }
});