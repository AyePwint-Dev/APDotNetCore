const tblCart = "carts";
let productId = null;

getCartTable();

function clearControls() {
  $("#txtName").val("");
  $("#txtDescription").val("");
  $("#txtPrice").val("");
  $("#txtName").focus();
}

function createCartItem(title, description, price) {
  let lst = getCartItems();
  const requestModel = {
    id: crypto.randomUUID(),
    title: title,
    description: description,
    price: price,
  };
  lst.push(requestModel);
  const jsonProduct = JSON.stringify(lst);
  localStorage.setItem(tblCart, jsonProduct);
  successMessage("Saving Successful.");
  clearControls();
}

function editCartItem(id) {
  let lst = getCartItems();
  const items = lst.filter((x) => x.id === id);
  if (items.length == 0) {
    console.log("no data found.");
    errorMessage("no data found.");
    return;
  }
  let item = items[0];
  productId = item.id;
  $("#txtName").val(item.title);
  $("#txtDescription").val(item.description);
  $("#txtPrice").val(item.price);
  $("#txtName").focus();
}

function updateCartItem(id, title, description, price) {
  let lst = getCartItems();
  const index = lst.findIndex((x) => x.id === id);
  if (index === -1) {
    console.log("no data found.");
    errorMessage("no data found.");
    return;
  }
  lst[index] = { id, title, description, price };
  const jsonCart = JSON.stringify(lst);
  localStorage.setItem(tblCart, jsonCart);
  successMessage("Updating Successful.");
}

function deleteCartItem(id) {
  Swal.fire({
    title: "Confirm",
    text: "Are you sure want to delete?",
    icon: "warning",
    showCancelButton: true,
    confirmButtonText: "Yes",
  }).then((result) => {
    if (!result.isConfirmed) return;
    let lst = getCartItems();
    const items = lst.filter((x) => x.id === id);
    if (items.length == 0) {
      console.log("no data found.");
      return;
    }
    lst = lst.filter((x) => x.id !== id);
    const jsonProduct = JSON.stringify(lst);
    localStorage.setItem(tblCart, jsonProduct);
    successMessage("Deleted Successfully!");
    getCartTable();
  });
}

function getCartItems() {
  const products = localStorage.getItem(tblCart);
  if (products === null) return [];
  return JSON.parse(products);
}

$("#btnCancel").click(function () {
  clearControls();
});

$("#btnSave").click(function () {
  const title = $("#txtName").val();
  console.log(title);
  const description = $("#txtDescription").val();
  const price = $("#txtPrice").val();
  if (productId === null) {
    createCartItem(title, description, price);
  } else {
    updateCartItem(productId, title, description, price);
    productId = null;
  }
  getCartTable();
});

function getCartTable() {
  const lst = getCartItems();
  let count = 0;
  let htmlRows = "";
  lst.forEach((item) => {
    const htmlRow = `
        <tr>
            <td>
                <button type="button" class="btn btn-warning" data-id="${
                  item.id
                }" onclick="editCartItem('${item.id}')">Edit</button>
                <button type="button" class="btn btn-danger" data-blog-id="${
                  item.id
                }" onclick="deleteCartItem('${item.id}')">Delete</button>
            </td>
            <td>${++count}</td>
            <td>${item.title}</td> <!-- Updated to use 'title' -->
            <td>${item.description}</td>
            <td>${item.price}</td>
        </tr>
        `;
    htmlRows += htmlRow;
  });

  $("#tbody").html(htmlRows);
}