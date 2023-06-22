class AuthDataService {
    constructor() {
    }

    login(formData) {
        const form = new FormData();
        form.append("username", "ass");
        form.append("password", "ass");
        return fetch("http://backend.localhost/login", {
            method: "POST",
            headers: {
                "accept": "*/*",
                "Content-Type": "multipart/form-data"
            },
            body: form
        });

    }

    logout() {
        localStorage.clear();
    }
}
