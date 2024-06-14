const tblBlog = "blogs";
let blogId = null;
// createBlog();
// UpdateBlog("c46c600d-8de9-40b3-b618-ba9658e123ad","title_3","author_3","content_3");
// deleteBlog("a8976f08-1679-49a9-98d5-a40246293be0");
readBlog();
getBlogTable();

function readBlog(){
    let lst = getBlog();
    const jsonBlog = JSON.stringify(lst);
    console.log(jsonBlog);
}

function editBlog(id){
    let lst = getBlog();
    const items = lst.filter(x=> x.id === id);
    if(items.length === 0){
        console.log("No data found");
        errorMessage("No data found");
        return;
    }
    let item = items[0];
    blogId = item.id;
    $('#txtTitle').val(item.title);
    $('#txtAuthor').val(item.author);
    $('#txtContent').val(item.content);
    
}

function createBlog(title, author, content){
    let lst = getBlog();
    const requestModel=
    {
        id:uuidv4(),
        title: title,
        author: author,
        content: content
    };
    lst.push(requestModel);
        const jsonBlog = JSON.stringify(lst);
        localStorage.setItem(tblBlog,jsonBlog);
    successMessage("Saving Successful");
    clearControl();
    console.log(jsonBlog);
}
function UpdateBlog(id, title, author, content){
    let lst = getBlog();
    const items = lst.filter(x=> x.id === id);
    if(items.length === 0){
        console.log("No data found to update");
        return;
    }
    const item = items[0];
    item.id = id;
    item.title = title;
    item.author = author;
    item.content = content;

    const index = lst.findIndex(x => x.id === id);
    lst[index] = item;

    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog,jsonBlog);
    successMessage("Updating successful!");
}
function deleteBlog(id){
    confirmMessage("Are you sure want to delete?").then(
        function (value) {
            let lst = getBlog();

            lst = lst.filter(x => x.id !== id);
            const jsonBlog = JSON.stringify(lst);
            localStorage.setItem(tblBlog, jsonBlog);

            successMessage("Deleting Successful.");

            getBlogTable();
        }
    );
}

function deleteBlog3(id){
    // let result = confirm("Are you sure you want to delete?");
    // if(!result) return;

    Swal.fire({
        title: "Confirm",
        text: "Are you sure you want to delete",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes"
      }).then((result) => {
        if (!result.isConfirmed) return;
      

    let lst = getBlog();
    lst = lst.filter(x => x.id !== id);
    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog,jsonBlog);

    successMessage("Deleteion Successful");
    getBlogTable();
        });
}


function deleteBlog2(id){
    let result = confirm("Are you sure you want to delete?");
    if(!result) return;

    let lst = getBlog();
    lst = lst.filter(x => x.id !== id);
    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog,jsonBlog);

    successMessage("Deleteion Successful");
    getBlogTable();
}

function getBlog(){
    const blogs = localStorage.getItem(tblBlog);
    let lst =[];
    if(blogs !== null){
        lst = JSON.parse(blogs);
    }
    return lst;
}

function testConfirmMessage() {
    let confirmMessage = new Promise(function (success, error) {
        // "Producing Code" (May take some time)
        const result = confirm('Are you sure want to delete?');
        if (result) {
            success(); // when successful
        } else {
            error();  // when error
        }
    });

    // "Consuming Code" (Must wait for a fulfilled Promise)
    confirmMessage.then(
        function (value) {
            /* code if successful */
            successMessage("Success");
        },
        function (error) {
            /* code if some error */
            errorMessage("Error");
        }
    );
}

$('#btnSave').click(function(){
    const title = $('#txtTitle').val();
    const author = $('#txtAuthor').val();
    const content = $('#txtContent').val();
    if(blogId === null){
        createBlog(title, author, content);
    }
    else{
        UpdateBlog(blogId, title, author, content);
        blogId = null;
    }
    getBlogTable();
})

function clearControl(){
    $('#txtTitle').val('');
    $('#txtAuthor').val('');
    $('#txtContent').val('');
}

function getBlogTable(){
    let lst = getBlog();
    let count = 0;
    let htmlRows ='';
    lst.forEach(item => {
        const htmlRow = `
        <tr>
            <td>
                <button type="button" class="btn btn-warning" onclick="editBlog('${item.id}')">Edit</button>
                <button type="button" class="btn btn-danger" onclick="deleteBlog('${item.id}')">Delete</button>
            </td>
            <td>${++count}</td>
            <td>${item.title}</td>
            <td>${item.author}</td>
            <td>${item.content}</td>
        </tr>
         `;
         htmlRows += htmlRow;
    });
    $('#tbody').html(htmlRows);
}