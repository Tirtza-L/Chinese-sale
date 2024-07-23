import axios from 'axios';

axios.defaults.baseURL = "http://localhost:5111"

export async function getAllCustomers() {
   return await axios.get('/Customer/getAllCustomer')
    .then((response) => {
        console.log(response)
        return response.data
    })
    .catch((e) => {
        console.log(e)
        return 0
    })
}
export async function getCustomerById(customerId) {
    return await axios.get(`/Customer/getCustomerById?CustomerId=${customerId}`)
     .then((response) => {
         console.log(response)
         return response.data
     })
     .catch((e) => {
         console.log(e)
         return 0
     })
 }
export async function addCustomer(customerDto) {

   return await axios.post('/Customer/addCustomer',customerDto)
        .then((response) => {
            console.log('response_addCustomer',response)
            return response
        })
        .catch((e) => {
            console.log(e)
            return 0
        })
}

export async function deleteCustomer(customerId) {
    await axios.delete('/Customer/deleteCustomer',customerId)
        .then((response) => {
            return response.data.statusCode
        })
        .catch((e) => {
            console.log(e)
        })
}

export async function Login(name,password) {
    const LoginDto={name,password}
    return await axios.post('/Customer/Login',LoginDto)
        .then(async(response) => {
            console.log("responseA",response);
            return response
        })
        .catch((e) => {
            return console.log("catchToken",e)
        })
}