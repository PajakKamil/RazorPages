var dataTable;

$(Document).ready(function () {
	dataTable = $('#DT_load').DataTable({
		"ajax": {
			"url": "/api/menuitem",
			"type": "GET",
			"datatype": "json"
		},
		"columns": [
			{ "data": "name", "width": "25%" },
			{ "data": "price", "width": "15%" },
			{ "data": "category.name", "width": "15%" },
			{ "data": "food.name", "width": "15%" },
			{
				"data": "id",
				"render": function (data) {
					return `<div class="w-75 btn-group">
							<a href="/Admin/MenuItems/Upsert?id=${data}" class="btn btn-success text-dark mx-1"
								style="cursor:pointer; width:100px;"> <i class="bi bi-pencil-square"></i></a>
							<a onclick="Delete('/api/MenuItem/'+${data})" class="btn btn-danger text-dark mx-1"
								style="cursor:pointer; width:100px;"> <i class="bi bi-trash-fill"></i></a>
							</div>`
				},
				"width": "15%"
			}



		],
		"width": "100%"
	});
});

function Delete(url) {
	Swal.fire({
		title: 'Are you sure?',
		text: "You won't be able to revert this!",
		icon: 'warning',
		showCancelButton: true,
		confirmButtonColor: '#3085d6',
		cancelButtonColor: '#d33',
		confirmButtonText: 'Yes, delete it!'
	}).then((result) => {
		if (result.isConfirmed) {
			$.ajax({
				url: url,
				type: 'DELETE',
				success: function (data) {
					if (data.success == true) {
						dataTable.ajax.reload();
						//success notification
						//toastr.success(data.message)
						Swal.fire(
							'Deleted!',
							'Your file has been deleted.',
							'success'
						)
					}
					else {
						toastr.error(data.message)
						//failture notification 
					}
				}
			})
		}
	})
}