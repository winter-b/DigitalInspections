<template>
    <div class="container">
    <div class="machine-create-view">
    <button @click="goBack()" class="btn btn-primary" style="float: left">Atgal</button>
      <h2>Nauja įranga</h2>
      <form @submit.prevent="saveMachine">
        <div>
          <label for="name">Pavadinimas:</label>
          <input type="text" id="name" v-model="formData.name" required>
        </div>
        <div>
          <label for="category">Kategorija:</label>
          <input type="text" id="category" v-model="formData.category" required>
        </div>
        <div>
          <label for="description">Aprašymas:</label>
          <textarea id="description" v-model="formData.description" required></textarea>
        </div>
        <div>
          <button type="submit" class="btn btn-success">Išsaugoti</button>
        </div>
      </form>
    </div>
</div>
  </template>
  
  <script>
  export default {
    data() {
      return {
        formData: {
          name: '',
          category: '',
          description: ''
        }
      };
    },
    methods: {
        goBack() {
        this.$router.push({ name: 'home' });
        },
      async saveMachine() {
        const form = new FormData();
        form.append("Name", this.formData.name);
        form.append("Description", this.formData.description);
        form.append("Category", this.formData.category);
        await fetch("http://backend.localhost/Machine", {
            method: "POST",
            headers: {
                "accept": "text/plain",
                "token": localStorage.getItem("token")
            },
            body: form
        });
        this.$router.push({ name: "home" });
      }
    }
  };
  </script>
  
  <style scoped>
  .machine-create-view {
    padding: 20px;
  }
  
  form {
    margin-top: 20px;
  }
  
  label {
    display: block;
    margin-bottom: 5px;
  }
  
  input[type="text"],
  textarea {
    width: 100%;
    padding: 8px;
    font-size: 16px;
    border: 1px solid #ccc;
    border-radius: 4px;
  }
  

  </style>