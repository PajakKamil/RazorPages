var dataTable;

$(Document).ready(function () {
	var url = window.location.search;
	if (url.includes("cancelled")) {
		loadList("cancelled")
	}
	else if (url.includes("ready")) {
		loadList("ready")
	}
	else if (url.includes("completed")) {
		loadList("completed")
	}
	else {
		loadList("inProcess")
	}
});

function loadList(param) {
	dataTable = $('#DT_load').DataTable({
		"ajax": {
			"url": "/api/orderList?status="+param,
			"type": "GET",
			"datatype": "json"
		},
		"columns": [
			{ "data": "id", "width": "10%" },
			{ "data": "pickUpName", "width": "15%" },
			{ "data": "applicationUser.email", "width": "15%" },
			{ "data": "orderTotal", "width": "15%" },
			{ "data": "pickUpTime", "width": "25%" },
			{
				"data": "id",
				"render": function (data) {
					return `<div class="w-75 btn-group">
							<a href="/Admin/Order/OrderDetails?id=${data}" class="btn btn-success text-dark mx-1"
								style="cursor:pointer; width:100px;"> <i class="bi bi-pencil-square"></i></a>
							</div>`
				},
				"width": "15%"
			}



		],
		"width": "100%"
	});
}