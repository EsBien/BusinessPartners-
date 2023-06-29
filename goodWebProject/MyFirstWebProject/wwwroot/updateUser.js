

function load() {
    
 let user = JSON.parse(sessionStorage.getItem('user'))
    document.getElementById("emailUpdate").value = user.email;
    document.getElementById("firstNameUpdate").value = user.firstName;
    document.getElementById("lastNameUpdate").value = user.lastName;
    document.getElementById("phoneUpdate").value = user.phoneNumber;
    document.getElementById("passwordUpdate").value = user.passwordCust;
    
}
function uptade() {
    let user1 = JSON.parse(sessionStorage.getItem('user'))
    let Customer = {
        Id: user1.userId,
        firstName: document.getElementById("firstNameUpdate").value,
        lastName: document.getElementById("lastNameUpdate").value,
        phoneNumber: document.getElementById("phoneUpdate").value,
        passwordCust: document.getElementById("passwordUpdate").value,
        email: document.getElementById("emailUpdate").value

    };
    //Customer.email.disabled;
    fetch("api/Customer/"+Customer.email, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(Customer)
    }).then((data) => {
        if (data.ok) {
            alert("user was updated successfully" + Customer.firstName)

        }
        else
            throw new Error("can`t updated user ")
    })
        


        //.catch(error => {
        //    alert(error);
        //})
}