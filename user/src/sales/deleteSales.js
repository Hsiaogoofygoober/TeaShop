import { api } from '../api';

function DeleteSales(id){
    const url = api.API_URL+'sales/'+id;
    console.log(url);
    const deleteOrder = async () => {
        try {
            const response = await fetch(url, {
                method: 'delete'
            })

            const json = await response.json();
            
            console.log(json);
        }
        catch (error) {
            console.log("error", error);
        }
    }
    deleteOrder();
    window.location.reload(); 
}

export default DeleteSales