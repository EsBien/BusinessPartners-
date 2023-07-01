
function loadVehicle() {
    let ListVehicle = JSON.parse(sessionStorage.getItem('VehicleList'));
    for (let i in ListVehicle)
        document.write(JSON.stringify(ListVehicle[i]))
}

function getVehicle(idType) {
 
    fetch("api/vehicle/getVehicleByType/" + idType).
        then(res => {
            if (res.ok) {
                
                alert("db reseved the list of cars succesefully")
                return res.json()
            }
            else
                alert("error server did nod reseved the list of cars ")
        })
        .then(data => {
            if (data) {
                sessionStorage.setItem('VehicleList', JSON.stringify(data))
                window.location.href = "vehicleType.html"
            }

        }).catch(e => alert(e))
   
}
