// Función que consulta a la API de cédulas y pinta en pantalla el nombre
const ConsultarNombre = () => {
    let cedula = $("#Cedula").val();
    $("#Nombre").val("");

    
    // Si la cedula no tiene AL MENOS 9 digitos, no se realiza la consulta
    if (cedula.length >= 9) {
        $.ajax({
            url: `https://apis.gometa.org/cedulas/${cedula}`,
            method: 'GET',
            dataType: "json",
            success: (data) => {
                $("#Nombre").val(data.nombre)
            }
        })
    }
}

