class WorkDataService {
    getWorks(machineId) {
        return fetch("http://backend.localhost/Machine/Works?machineId=${machineId}&limit=10&offset=0", {
            headers: {
            "accept": "text/plain",
            "token": localStorage.getItem("token")
            }
        })
    }
    
    get(id) {
        return fetch("http://backend.localhost/Machine/Work?id=${id}", {
            headers: {
            "accept": "text/plain",
            "token": localStorage.getItem("token")
            }
        })
    }

    create(formData) {
        const form = new FormData();
        form.append("machineId", "9a2729f5-075f-4937-bee9-6b652f77a66e");
        form.append("nextInspection", "2030-06-15T13:45:30");
        form.append("name", "ass");
        form.append("description", "ass");
        form.append("category", "ass");

        fetch("http://backend.localhost/Machine/Work", {
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
        form.append("nextInspection", "2039-06-15T13:45:30");
        form.append("isInspected", "true");
        form.append("name", "assas");
        form.append("description", "assas");
        form.append("category", "assas");

        fetch("http://backend.localhost/Machine/Work?workId=${id}", {
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
        fetch("http://backend.localhost/Machine/Work?workId=${id}", {
            method: "DELETE",
            headers: {
              "accept": "text/plain",
              "token": localStorage.getItem("token")
            }
          });
    }
    
}

export default new WorkDataService();