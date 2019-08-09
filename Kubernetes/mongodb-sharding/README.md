# Mongodb sharding cluster on Windows, Docker desktop, Kubernetes
Based on https://github.com/cicciodifranco/kubernetes-mongo-shard-cluster
I implemented powershell script that enable run mongodb shard cluster on Windows, Docker desktop, Kubernetes


## Deploy cluster
In your master node do

```PowerShell
set-executionpolicy remotesigned #enable script run
```

```PowerShell 
&"./initiate.ps1"
```
`initiate.ps1` will deploy for you all pods and will create all needed services.

## Connecting to mongo
Enable port-forward so that you can use tool on host (like robo 3T to connect to Mongos on cluster for testing)
Note: <mongos-cluster-id> is the one from kubectl get pods
```PowerShell 
kubectl port-forward mongos1-<mongos-cluster-id> 37017:27017
```


## Clean

To remove all pods
```PowerShell
&"./clean.ps1"
```

## Deploy different number of shard replica set

- Edit in `config.ps1` file `SHARD_REPLICA_SET` and set the desired number
- Create for each additional replica set a file named `mongo_sh_N.yaml`
- Copy content from `mongo_sh_1.yaml`
- Replace all occurrences of `mongosh1` with `mongoshN`  
- Replace all occurrences of `rs1` with `rsN`
