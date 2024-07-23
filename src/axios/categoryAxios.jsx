import axios from 'axios';

axios.defaults.baseURL = "http://localhost:5111"

export async function getAllCategories() {
   return await axios.get('/Category/getAll')
    .then((response) => {
        console.log(response)
        return response.data
    })
    .catch((e) => {
        console.log(e)
        return 0
    })
}

// export async function getCategoryByName(name) {
//     debugger
//     name=String(name)
//     return await axios.get('/Category/getByName',name)
//      .then((response) => {
//          console.log(response)
//          return response.data
//      })
//      .catch((e) => {
//          console.log(e)
//          return 0
//      })
//  }

export async function addCategory(categoryDto) {
    debugger
   return await axios.post('/Category/add',categoryDto)
        .then((response) => {
            console.log(response)
            return response
        })
        .catch((e) => {
            console.log(e)
            return 0
        })
}

export async function deleteCategory(categoryId) {
    await axios.delete('/Category/delete',categoryId)
        .then((response) => {
            return response.data.statusCode
        })
        .catch((e) => {
            console.log(e)
        })
}