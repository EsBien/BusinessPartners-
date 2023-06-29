

function login() {


    let email = document.getElementById("email").value;
    let password = document.getElementById("password").value;
    fetch("api/Customer/" + email + "/" + password)
        .then((res) => {
            if (res.ok) {
                if (res.statusText != "No Content")
                    return res.json()
                else
                    throw Error("no user in data base of server")
            }
            else {
                throw new Error("no user in data base ")
            }
        })
        .then((data) => {
            sessionStorage.setItem('user', JSON.stringify(data))
            alert("welcom to " + data.firstName + " " +data.lastName);
            window.location.href = "updateUser.html"

        })
        .catch(error => {
            alert(error);
        })
}


function addNowUser() {
    document.getElementById("newUser").style.display = "block"

}
function saveUser() {

    let Customer = {
        //UserId:0,
        firstName: document.getElementById("firstName1").value,
        lastName: document.getElementById("lastName1").value,
        passwordCust: document.getElementById("password1").value,
        phoneNumber: document.getElementById("phoneNumber1").value,
        email: document.getElementById("email1").value
    };

    fetch("api/Customer", {
        method: 'POST',
       
        headers: {
            'Content-Type': 'application/json'
        } ,
        body: JSON.stringify(Customer)
    }).then((data) => {
        if (data.ok) 
            return data.json()
        else
            throw Error("can`t add user ")
    })
        .then(res => { alert("Customer " + res.firstName +" was added successfully") })
        
//        .catch(error => {
//            alert(error);
//        })
}
/////////##car rental###########/////////


//function getVehicle(idType) {

//    fetch("api/vehicle/getVehicleByType/" + idType).
//        then(res => {
//            if (res.ok) {
                
//                alert("db reseved the list of cars succesefully")
//                return res.json()
//            }
//            else
//                alert("error server did nod reseved the list of cars ")
//        })
//        .then(data => {
//            if (data) {
//                sessionStorage.setItem('VehicleList', JSON.stringify(data))
//                window.location.href = "vehicleType.html"
//            }

//        }).catch(e => alert(e))

//}
