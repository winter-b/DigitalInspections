<template>
    <div class="container">
    <div class="work-edit-view">
    <div class="work-actions">
    <button @click="goBack()" class="btn btn-primary" style="float: left">Atgal</button>
    </div>  
    <h2>Darbo atnaujinimas</h2>
      <form @submit.prevent="saveWork">
        <div class="form-group">
          <label for="name">Pavadinimas</label>
          <input type="text" id="name" v-model="work.name" required>
        </div>
        <div class="form-group">
          <label for="category">Kategorija</label>
          <input type="text" id="category" v-model="work.category" required>
        </div>
        <div class="form-group">
          <label for="description">Aprašymas</label>
          <textarea id="description" v-model="work.description"></textarea>
        </div>
        <div class="form-group">
          <label for="nextInspection">Pabaigti iki</label>
          <input type="date" id="nextInspection" v-model="work.nextInspection">
        </div>
        <div class="form-group">
          <label for="isInspected">Pabaigta</label>
          <input type="checkbox" id="isInspected" v-model="work.isInspected">
        </div>
        <button type="submit" class="btn btn-success">Išsaugoti</button>
      </form>
    </div>
</div>
  </template>
  
  <script>
  import router from "@/router";
  export default {
    data() {
      return {
        work: {
          nextInspection: '',
          name: '',
          description: '',
          category: '',
          isInspected: false,
        },
      };
    },
    mounted() {
    this.fetchWorkData();
    },
    methods: {
        goBack() {
            router.push({ name: "machine", params: { id: this.work.machineId } });
        },
      saveWork() {
        var formData = new FormData();
        formData.append("nextInspection", this.work.nextInspection);
        formData.append("name", this.work.name);
        formData.append("description", this.work.description);
        formData.append("category", this.work.category);
        formData.append("isInspected", this.work.isInspected);
        fetch(`http://backend.ogversion.com/Machine/Work?workId=${this.$route.params.id}`, {
        method: "PUT",
        headers: {
          "accept": "text/plain",
          "token" : localStorage.getItem("token"),
        },
         body: formData,
        }).then(() => {
        router.push({ name: "machine", params: { id: this.work.machineId } });
        });
      },
      async fetchWorkData() {
      try {
        const response = await fetch(
        `http://backend.ogversion.com/Machine/Work?workId=${this.$route.params.id}`,
        {
          method: "GET",
          headers: {
            "accept": "text/plain",
            "token" : localStorage.getItem("token"),
          },
        }
        ).then((response) => response.json());
        if (response.statuscode === 401) {
          router.push({ name: "login" });
        }
        this.work = response;
      } catch (error) {
        console.error(error);
      }
    },
    },
  };
  </script>
  
  <style scoped>
  .work-edit-view {
    flex-direction: column;
    align-items: center;
    justify-content: center;
    padding: 20px;
  }
  
  .work-edit-view h2 {
    margin-bottom: 20px;
    text-align: center;
  }
  
  .work-edit-view form {
    width: 100%;
  }
  
  .work-edit-view .form-group {
    margin-bottom: 20px;
  }
  
  .work-edit-view label {
    display: block;
    margin-bottom: 5px;
  }
  
  .work-edit-view input,
  .work-edit-view textarea {
    width: 100%;
    padding: 10px;
    border: 1px solid #ccc;
    border-radius: 4px;
  }
  

  .back-button {
    flex: left;
  }
  .work-actions {
  text-align: right;
  float: left;
  margin-bottom: 10px;
}
  </style>