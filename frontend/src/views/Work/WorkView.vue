<template>
    <div class="container">
    <div class="work-view"> 
        <div class="work-actions">
        <button @click="goBack()" class="btn btn-primary" style="float: left">Atgal</button>
        <button @click="editWork(work.id)" class="btn btn-warning">Atnaujinti</button>
        <button v-if="!work.isInspected" class="btn btn-success" @click="markAsInspected(work.id)">Pažymėti kaip pabaigtą</button>
        <button v-if="work.isInspected" class="btn btn-danger" @click="markAsUnInspected(work.id)">Pažymėti kaip NEpabaigtą</button>
        </div>
      <div class="work-details">
        <div class="work-info">
        <div style="display: flex; justify-content: space-between;">
            <h2>{{ work.name }}</h2>
          <p>Atnaujintas: {{ new Date(work.lastUpdated).toLocaleString() }}</p>
        </div>
          <p>Kategorija: {{ work.category }}</p>
          <p>Autorius: {{ work.addedBy }}</p>
          <p>Pabaigti iki: {{ new Date(work.nextInspection).toLocaleDateString() }}</p>
          <p>Pabaigtas: {{ work.isInspected ? "Taip" : "Ne" }}</p>
        </div>
        <div class="work-description">
          <h3>Aprašymas:</h3>
          <p>{{ work.description }}</p>
        </div>
      </div>
    </div>
</div>
  </template>

<script>
import router from "@/router";

export default {
  data() {
    return {
      work: {},
    };
  },
  mounted() {
    this.fetchWorkData();
  },
  methods: {
    goBack() {
      this.$router.push(`/machine/${this.$route.params.machineId}`);
    },
    async fetchWorkData() {
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
        this.work = response;
    },
    markAsInspected(workId) {
    var formData = new FormData();
    formData.append("isInspected", "true");
    fetch(`http://backend.ogversion.com/Machine/Work?workId=${workId}`, {
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
    markAsUnInspected(workId) {
    var formData = new FormData();
    formData.append("isInspected", "false");
    fetch(`http://backend.ogversion.com/Machine/Work?workId=${workId}`, {
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
    editWork(workId) {
      router.push({ name: "workedit", params: { id: workId } });
    },
  },
  
};
</script>  
  <style scoped>
  .work-actions {
  text-align: right;
  margin-bottom: 10px;
}
button{
    margin-right: 20px;
}
  .work-view {
    margin: 20px;
  }
  
  .work-details {
    border: 3px solid black;
    padding: 20px;
  }
  
  .work-info {
    margin-bottom: 10px;
  }
  
  .work-description {
    margin-top: 20px;
  }
  
  @media (max-width: 768px) {
    .work-details {
      padding: 10px;
    }
  }
  .back-button {
  float: left;
}
  </style>