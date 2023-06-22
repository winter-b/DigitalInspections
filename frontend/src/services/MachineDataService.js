class MachineDataService {
    getAll() {
        return fetch("http://backend.localhost/Machine&limit=10&offset=0", {
            headers: {
            "accept": "text/plain",
            "token": localStorage.getItem("token")
        }
        })
    }
    
    get(id) {
        return fetch("http://backend.localhost/Machine?id=${id}", {
            headers: {
            "accept": "text/plain",
            "token": localStorage.getItem("token")
            }
        })
    }
    
    create(formData) {
        const form = new FormData();
        form.append("Name", "ass");
        form.append("Description", "ass");
        form.append("Category", "ass");
        return fetch("http://backend.localhost/Machine", {
            method: "POST",
            headers: {
                "accept": "text/plain",
                "token": localStorage.getItem("token"),
                "Content-Type": "multipart/form-data"
            },
            body: form
        });
    }
    
    update(id, formData) {
        const form = new FormData();
        form.append("Name", "ass");
        form.append("Description", "ass");
        form.append("Category", "ass");
        return fetch("http://backend.localhost/Machine?machineId=${id}", {
            method: "PUT",
            headers: {
                "accept": "text/plain",
                "token": localStorage.getItem("token"),
                "Content-Type": "multipart/form-data"
            },
            body: form
        });
    }
    
    delete(id) {
        return fetch("http://backend.localhost/Machine?machineId=${id}", {
            method: "DELETE",
            headers: {
                "accept": "text/plain",
                "token": localStorage.getItem("token"),
                "Content-Type": "multipart/form-data"
            }
        })
    }
}
    
    